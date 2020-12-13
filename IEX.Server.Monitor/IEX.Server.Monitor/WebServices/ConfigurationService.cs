using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace IEX.Server.Monitor
{
    using IEX.Utilities;
    using IEX.Utilities.Collections;
    using IEX.Utilities.Tools;
    using IEX.Server.ServiceManagement;
    using IEXConfiguration = IEX.Server.Configuration;
    using IEX_SM_MD = IEX.Server.ServiceManagement.MetaData;
    using IEXConfigurationSC = IEX.Server.Monitor.Configuration.ServerConfiguration;
    using IEX.Server.Monitor.ServerServiceReference;
    using IEX.Server.Monitor.Configuration.ServerConfiguration;
    using IEX.Server.ServiceManagement.MetaData;
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [IEX.Utilities.ServiceBehavior.MethodsErrorBehavior]
    public class ConfigurationService : IConfigurationService
    {
        public int IexId { get; set; }

        #region IConfigurationService
        public IEXConfigurationSC.IexServerConfiguration GetServerConfiguration(int id)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id });
            IEXConfigurationSC.IexServerConfiguration result = null;
            IEXConfiguration.IexServerConfiguration configuration = GetConfiguration(id);
            if (configuration != null)
                result = new IEXConfigurationSC.IexServerConfiguration(configuration);
            else
                result = new IEXConfigurationSC.IexServerConfiguration(null);

            foreach (Type type in GetImplementations())
            {
                IEX_SM_MD.Implementation implementation = new IEX_SM_MD.Implementation(type);
                if (implementation.AutomaticConfiguration && AutomaticConfiguration.Configurated(type, id))
                {
                    IEX_SM_MD.Service service = SubsystemsMetadataServiceByImplementationName(implementation.ImplementationName);
                    string[] values = AutomaticConfiguration.DefaultValues(type, id);
                    if (values == null)
                        values = new string[implementation.Parameters.Count].Select(item => string.Empty).ToArray();
                    IEXConfigurationSC.ParamInfoCollection parameters = new IEXConfigurationSC.ParamInfoCollection((IEnumerable<IEXConfigurationSC.ParamInfo>)(Array.ConvertAll(values, item => new IEXConfigurationSC.ParamInfo(item))));
                    IEXConfigurationSC.InstanceInfo[] current_instances_info = result.Find(service.ServiceName, implementation.ImplementationName);
                    bool equal_automatic_values = false;
                    if (current_instances_info != null)
                    {
                        for (int j = 0; j < current_instances_info.Length && equal_automatic_values == false; ++j)
                        {
                            IEXConfigurationSC.InstanceInfo current_instance_info = current_instances_info[j];
                            equal_automatic_values = true;
                            for (int i = 0; i < parameters.Count; ++i)
                            {
                                if (!string.IsNullOrEmpty(parameters[i].Value))
                                {
                                    if (parameters[i].Value != current_instance_info.Parameters[i].Value)
                                    {
                                        equal_automatic_values = false;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    if (equal_automatic_values == false)
                    {
                        System.Reflection.ParameterInfo[] params_info = IEX.Server.ServiceManagement.Attributes.Utils.GetIexConstructorParametersInfo(type);
                        IEXConfigurationSC.ParamInfoCollection implementation_parameters = new IEXConfigurationSC.ParamInfoCollection((IEnumerable<IEXConfigurationSC.ParamInfo>)(Array.ConvertAll(params_info, item => new IEXConfigurationSC.ParamInfo(item.DefaultValue.ToString()))));
                        for (int i = 0; i < parameters.Count; ++i)
                        {
                            if (!string.IsNullOrEmpty(parameters[i].Value))
                                implementation_parameters[i] = parameters[i];
                        }
                        IEXConfigurationSC.ServiceInfo service_info = result.Find(service.ServiceName);
                        string instance_name = service.DisplayName;
                        if (service_info != null)
                            instance_name = GetName(service_info.Instances.ToArray(), service.DisplayName);
                        IEXConfigurationSC.InstanceInfo instance_info = new IEXConfigurationSC.InstanceInfo(instance_name, implementation.ImplementationName, implementation_parameters) { AutomaticConfiguration = true };
                        result.Add(service.ServiceName, instance_info);
                    }
                }
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }
        
        private void BeforeSave(IEX.Server.ServiceManagement.MetaData.ServiceCollection metadata, IEX.Server.Configuration.IexServerConfiguration configuration)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { metadata, configuration });
            foreach (IEXConfiguration.ServiceInfo service_info in configuration.Service)
            {
                IEX_SM_MD.Service service = metadata.Find(item => item.ServiceName == service_info.Name);
                foreach (IEXConfiguration.InstanceInfo instance_info in service_info.Instance)
                {
                    IEX_SM_MD.Implementation implementation = service.Implementations.Find(item => item.ImplementationName == instance_info.Implementation);
                    if (implementation != null)
                    {
                        if (implementation.Parameters.Count == instance_info.Param.Length)
                        {
                            int i = 0;
                            foreach (IEXConfiguration.ParamInfo param_info in instance_info.Param)
                            {
                                var activities_before_save = implementation.Parameters[i].Activities.Where(item => item.Event == ActivityAttribute.Events.BeforeSave);
                                foreach (var activity_before_save in activities_before_save)
                                {
                                    System.Reflection.AssemblyName assembly_name = new System.Reflection.AssemblyName(activity_before_save.Assembly);
                                    System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assembly_name);
                                    object instance = assembly.CreateInstance(activity_before_save.Class);
                                    Type class_type = instance.GetType();
                                    System.Reflection.MethodInfo method = class_type.GetMethod(activity_before_save.Method);
                                    method.Invoke(instance, new object[] {param_info.Value });
                                }
                                i++;
                            }
                        }
                    }
                }
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + configuration);
        }

        public void SetServerConfiguration(int id, IEXConfigurationSC.IexServerConfiguration data)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, data });
            string folder_path = IEX.Utilities.IEXConfiguration.GetIexInstallationFolder();
            string full_path = GetConfigurationFullPath(id);
            IEX.Server.Configuration.IexServerConfiguration configuration = data.ToConfiguration();
            BeforeSave(GetSubsystemsMetadata(), configuration);
            IEX_SM_MD.Tools.SetParameters(GetSubsystemsMetadata(), configuration, true, false);
            IEXConfiguration.IexXmlGlue.SerializeIexServerConfiguration(full_path, configuration);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting");
        }

        private static IEX_SM_MD.ServiceCollection _meta_data = null;
        private static Type[] _classes = null;
        public IEX_SM_MD.ServiceCollection GetSubsystemsMetadata()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            if (_meta_data == null)
            {
                Type[] interfaces;
                _meta_data = IEX.Server.ServiceManagement.MetaData.Tools.SubSystemsMetadata(out interfaces, out _classes);
            }
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + _meta_data);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting");
            return _meta_data;
        }

        public string[] GetDefaultValues(int id, string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { id, implementation_name });
            string[] result = null;
            Type type = Array.Find(GetImplementations(), item => item.FullName == implementation_name);
            result = AutomaticConfiguration.DefaultValues(type, id);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + StringExtensions.Join(result));
            return result;
        }

        public int[] GetConfiguratedIdsServers()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            List<int> result = new List<int>();
            foreach (int id in IEX.Utilities.IEXConfiguration.GetServerIds())
            {
                string configuration_full_path = GetConfigurationFullPath(id);
                if (System.IO.File.Exists(configuration_full_path))
                    result.Add(id);
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + string.Join(",", result));
            return result.ToArray();
        }

        public int[] GetAutomaticConfiguratedIdsServers()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            List<int> result = new List<int>();
            int[] ids = IEX.Utilities.IEXConfiguration.GetServerIds();
            int[] configurated_ids = GetConfiguratedIdsServers();
            int[] array = ids.Where(id => !configurated_ids.Contains(id)).ToArray();
            foreach (int id in array)
            {
                foreach (Type type in GetImplementations())
                {
                    if (AutomaticConfiguration.ImplementAutomaticConfiguration(type) && AutomaticConfiguration.Configurated(type, id))
                    {
                        result.Add(id);
                        continue;
                    }
                }
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + string.Join(",", result));
            return result.ToArray();
        }

        public IEXConfigurationSC.IexServerConfiguration ConfigurationDeserialize(string data)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { data });
            IEXConfiguration.IexServerConfiguration configuration = IEX.Utilities.Tools.Serializer.Deserialize<IEXConfiguration.IexServerConfiguration>(data);
            IEXConfigurationSC.IexServerConfiguration result = new IEXConfigurationSC.IexServerConfiguration(configuration);
            Tracer.Write(Tracer.TraceLevel.INFO, "exiting. result is: " + result);
            return result;
        }

        public string ConfigurationSerialize(IEXConfigurationSC.IexServerConfiguration data)
        {
            Tracer.Write(Tracer.TraceLevel.INFO, "entered", new object[] { data });
            IEXConfiguration.IexServerConfiguration configuration = data.ToConfiguration();
            string result = IEX.Utilities.Tools.Serializer.Serialize(configuration);
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        #endregion IConfigurationService

        static private string GetConfigurationFullPath(int id)
        {
            return IEX.Utilities.IEXConfiguration.ConfigurationFullPath(id, IEX.Server.Configuration.IexXmlGlue.CONFIGURATION_FILE_NAME);
        }

        private IEX_SM_MD.Service SubsystemsMetadataServiceByImplementationName(string implementation_name)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "entered", new object[] { implementation_name });
            IEX_SM_MD.Service result = null;
            IEX_SM_MD.ServiceCollection subsystems_metadata = GetSubsystemsMetadata();
            var services = from item in subsystems_metadata
                           let implementations = item.Implementations
                           where
                           implementations.Any(implementation => implementation.ImplementationName == implementation_name)
                           select item;
            if (services != null && services.Any())
                result = services.ElementAt(0);
            Tracer.Write(Tracer.TraceLevel.DEBUG, "exiting. result is: " + result);
            return result;
        }

        static private IEXConfiguration.IexServerConfiguration GetConfiguration(int id)
        {
            string full_path = IEX.Utilities.IEXConfiguration.ConfigurationFullPath(id, IEX.Server.Configuration.IexXmlGlue.CONFIGURATION_FILE_NAME);
            return IEX.Server.ServiceManagement.ServiceManager.GetConfiguration(full_path);
        }

        static private bool ValidateName(IEnumerable<IEXConfigurationSC.InstanceInfo> instances_info, string instance_name)
        {
            if (instances_info == null)
                return true;
            return instances_info.IndexWhere(item => item.Name == instance_name) == -1;
        }

        static private string GetName(IEnumerable<IEXConfigurationSC.InstanceInfo> instances_info, string base_name)
        {
            int key = 0;
            while (ValidateName(instances_info, key == 0 ? base_name : string.Format("{0} {1}", base_name, key)) == false) { key++; }
            return key == 0 ? base_name : string.Format("{0} {1}", base_name, key);
        }
        
        private Type[] GetImplementations()
        {
            if (_classes == null)
                GetSubsystemsMetadata();
            return _classes;
        }

        public string GetConfigurationParameterValue(int iexNumber, string serviceName, string parameterName, string implementationName = "", string instanceName = "")
        {
            //validate mandatory parameters
            if (iexNumber <= 0) //reduced validation on iexNumber as defined per requirements
            {
                throw new IexServerArgumentException(string.Format("Invalid argument: iexNumber = {0} .", iexNumber));
            }

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new IexServerArgumentException(@"The ""serviceName"" argument cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new IexServerArgumentException(@"The ""parameterName"" argument cannot be empty.");
            }

            List<ParamInfo> listParamsData = null;
            int index = -1;

            //implementationName needed to search parameterName in metadata
            if (string.IsNullOrWhiteSpace(implementationName)) //implementationName not given by the user, so need to find it in data based on instance
            {
                //get implementationName from data
                IEXConfigurationSC.ServiceInfo serviceData = TryGettingServiceData(iexNumber, serviceName);
                IEXConfigurationSC.InstanceInfo instance = TryGettingInstance(serviceData, iexNumber, serviceName, instanceName);
                implementationName = instance.Implementation;
                
                //get parameters data list
                listParamsData = instance.Parameters;

                //find index of parameter in metadata list - will be the same index in corresponding data list
                index = TryGettingIndexOfParameterFromMetadata(serviceName, implementationName, parameterName);
            }
            else //implementationName given by the user, can be used to search parameterName in metadata
            {
                //find index of parameter in metadata list - will be the same index in corresponding data list
                index = TryGettingIndexOfParameterFromMetadata(serviceName, implementationName, parameterName);

                //get parameters data list
                IEXConfigurationSC.ServiceInfo serviceData = TryGettingServiceData(iexNumber, serviceName);
                List<IEXConfigurationSC.InstanceInfo> listInstances = TryGettingListInstances(serviceData, iexNumber, serviceName, implementationName);
                listParamsData = TryGettingListParamsData(iexNumber,serviceName, implementationName, listInstances, instanceName);
            }

            //TODO may validate that data list size fits metadata list size

            string value = listParamsData[index].Value;
            
            return value != null ? value : string.Empty;
        }

        private int TryGettingIndexOfParameterFromMetadata(string serviceName, string implementationName, string parameterName)
        {
            List<IEX_SM_MD.Parameter> listParamsMetadata = null;

            string serviceDisplayName = string.Empty;
            string implementationDisplayName = string.Empty;

            try
            {
                listParamsMetadata = GetServiceMetadata(serviceName, implementationName, out serviceDisplayName, out implementationDisplayName);
            }
            catch (NullReferenceException)
            {
                //as a shortcut, get more accurate message of the specific failure based on out parameters population
                if (string.IsNullOrEmpty(serviceDisplayName))
                {
                    throw new IexServerArgumentException(string.Format("You entered a serviceName {0} which seems to be invalid. No such service can be found in Configuration metadata.", serviceName));
                }
                else //service known so exception occured due to missing implementation
                {
                    throw new IexServerArgumentException(string.Format("The implementationName {1} cannot be found under serviceName {0}{2} in Configuration metadata.", serviceName, implementationName, EncloseInParentheses(serviceDisplayName)));
                }
            }
            catch (InvalidOperationException)
            {
                throw new IexServerArgumentException(string.Format("Multiple results found in Configuration metadata for serviceName {0}{2} and implementationName {1}{3}.", serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName)));
            }

            if (ExtensionHelpers.IsNullOrEmpty(listParamsMetadata))
            {
                throw new IexServerArgumentException(string.Format("No Configuration metadata found for serviceName {0}{2} and implementationName {1}{3}.", serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName)));
            }

            int index = listParamsMetadata.FindIndex(param => param.ParameterName.Equals(parameterName));

            if (index == -1)
            {
                throw new IexServerArgumentException(string.Format("{2} is not a valid Configuration metadata parameter name for serviceName {0}{3} and implementationName {1}{4}.", serviceName, implementationName, parameterName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName)));
            }

            return index;
        }

        private List<IEXConfigurationSC.InstanceInfo> TryGettingListInstances(IEXConfigurationSC.ServiceInfo serviceData, int iexNumber, string serviceName, string implementationName)
        {
            List<IEXConfigurationSC.InstanceInfo> listInstances = null;
            
            try
            {
                listInstances = GetListInstances(serviceData, implementationName);
                if (ExtensionHelpers.IsNullOrEmpty(listInstances))
                {
                    throw new NullReferenceException();//done assuming in this case the exact exception doesn't care, so throwing the already catched exception type
                }
            }
            catch (NullReferenceException)
            {
                //trying to add display name(s) from metadata for new requirement of more readable log message
                string serviceDisplayName;
                string implementationDisplayName;
                TryGettingServiceAndImpDisplayNamesFromMetadata(serviceName, implementationName, out serviceDisplayName, out implementationDisplayName);
                throw new IexServerArgumentException(string.Format("No data found in IEX {0} Configuration for serviceName {1}{3} and implementationName {2}{4}.", iexNumber, serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName)));
            }

            return listInstances;
        }

        private string EncloseInParenthesesIfNotEmptyStr(string encloseMeIfNotEmpty)
        {
            if (string.IsNullOrEmpty(encloseMeIfNotEmpty))
            {
                return string.Empty;
            }
            return EncloseInParentheses(encloseMeIfNotEmpty);
        }

        private string EncloseInParentheses(string encloseMe)
        {
            return new StringBuilder().Append(' ').Append('(').Append(encloseMe).Append(')').ToString();
        }

        private IEXConfigurationSC.InstanceInfo TryGettingInstance(IEXConfigurationSC.ServiceInfo serviceData, int iexNumber, string serviceName, string instanceName)
        {
            IEXConfigurationSC.InstanceInfo instance = null;
            bool emptyInstanceName = string.IsNullOrWhiteSpace(instanceName);

            try
            {
                instance = emptyInstanceName ? GetInstance(serviceData) : GetInstance(serviceData, instanceName);
            }
            catch (InvalidOperationException)
            {
                //trying to add service display name from metadata for new requirement of more readable log message
                string serviceDisplayName;
                TryGettingServiceDisplayNameFromMetadata(serviceName, out serviceDisplayName);
                throw new IexServerArgumentException(string.Format("Multiple instances found in IEX {0} Configuration data for serviceName {1}{2}" + ((!emptyInstanceName) ? " and instanceName {3}" : "") + '.', iexNumber, serviceName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), instanceName));
            }

            if (instance == null)
            {
                //trying to add service display name from metadata for new requirement of more readable log message
                string serviceDisplayName;
                TryGettingServiceDisplayNameFromMetadata(serviceName, out serviceDisplayName);
                //throw new IexServerArgumentException(string.Format("No instance found for serviceName {1}{2}" + ((!emptyInstanceName) ? " and instanceName {3}" : "") + " in IEX {0} Configuration data" + '.', iexNumber, serviceName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), instanceName));
                throw new IexServerArgumentException(string.Format("No instance found" + ((!emptyInstanceName) ? " with instanceName {3}" : "") + " under serviceName {1}{2} in IEX {0} Configuration data.", iexNumber, serviceName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), instanceName));
            }
            
            return instance;
        }

        

        private List<ParamInfo> TryGettingListParamsData(int iexNumber, string serviceName, string implementationName, List<IEXConfigurationSC.InstanceInfo> listInstances, string instanceName)
        {
            List<ParamInfo> listParamsData = null;
            bool emptyInstanceName = string.IsNullOrWhiteSpace(instanceName);

            try
            {
                listParamsData = emptyInstanceName ? GetListParamsData(listInstances) : GetListParamsData(listInstances, instanceName);
            }
            catch (NullReferenceException)
            {
                //trying to add display name(s) from metadata for new requirement of more readable log message
                string serviceDisplayName;
                string implementationDisplayName;
                TryGettingServiceAndImpDisplayNamesFromMetadata(serviceName, implementationName, out serviceDisplayName, out implementationDisplayName);
                throw new IexServerArgumentException(string.Format("No data found in IEX {0} Configuration data for serviceName {1}{3} and implementationName {2}{4}" + ((!emptyInstanceName) ? " and instanceName {5}" : "") + '.', iexNumber, serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName), instanceName));
            }
            catch (InvalidOperationException)
            {
                //trying to add display name(s) from metadata for new requirement of more readable log message
                string serviceDisplayName;
                string implementationDisplayName;
                TryGettingServiceAndImpDisplayNamesFromMetadata(serviceName, implementationName, out serviceDisplayName, out implementationDisplayName);
                //N/R to display here instanceName since multiple instances
                throw new IexServerArgumentException(string.Format("Multiple instances found in IEX {0} Configuration data for serviceName {1}{3} and implementationName {2}{4}.", iexNumber, serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName)));
            }

            if (ExtensionHelpers.IsNullOrEmpty(listParamsData))
            {
                //trying to add display name(s) from metadata for new requirement of more readable log message
                string serviceDisplayName;
                string implementationDisplayName;
                TryGettingServiceAndImpDisplayNamesFromMetadata(serviceName, implementationName, out serviceDisplayName, out implementationDisplayName);
                throw new IexServerArgumentException(string.Format("No data found in IEX {0} Configuration data for serviceName {1}{3} and implementationName {2}{4}" + ((!emptyInstanceName) ? " and instanceName {5}" : "") + '.', iexNumber, serviceName, implementationName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName), EncloseInParenthesesIfNotEmptyStr(implementationDisplayName), instanceName));
            }

            return listParamsData;
        }

        private List<ParamInfo> GetListParamsData(List<IEXConfigurationSC.InstanceInfo> listInstances, string instanceName)
        {
            return listInstances.Where(instance => string.Equals(instance.Name, instanceName)).SingleOrDefault().Parameters;
        }

        private List<ParamInfo> GetListParamsData(List<IEXConfigurationSC.InstanceInfo> listInstances)
        {
            return listInstances.SingleOrDefault().Parameters;
        }

        private List<IEXConfigurationSC.InstanceInfo> GetListInstances(IEXConfigurationSC.ServiceInfo serviceData, string implementationName)
        {
            return serviceData.Instances.Where(instance => instance.Implementation.Equals(implementationName)).ToList<IEXConfigurationSC.InstanceInfo>();
        }

        private void TryGettingServiceDisplayNameFromMetadata(string serviceName, out string serviceDisplayName)
        {
            serviceDisplayName = string.Empty;

            try
            {
                Service serviceMetadata = GetSubsystemsMetadata()
                    .Where(service => service.ServiceName.Equals(serviceName)).SingleOrDefault();
                serviceDisplayName = serviceMetadata.DisplayName;
            }
            catch (Exception)
            {
                //just avoiding exception - service display name is only "nice to have"
            }
        }

        private void TryGettingServiceAndImpDisplayNamesFromMetadata(string serviceName, string implementationName, out string serviceDisplayName, out string implementationDisplayName)
        {
            serviceDisplayName = string.Empty;
            implementationDisplayName = string.Empty;

            try
            {
                Service serviceMetadata = GetSubsystemsMetadata()
                    .Where(service => service.ServiceName.Equals(serviceName)).SingleOrDefault();
                serviceDisplayName = serviceMetadata.DisplayName;

                Implementation implementationMetadata = serviceMetadata.Implementations
                    .Where(impl => impl.ImplementationName.Equals(implementationName)).SingleOrDefault();
                implementationDisplayName = implementationMetadata.DisplayName;
            }
            catch(Exception)
            {
                //just avoiding exception - display names are only "nice to have"
            }
        }

        private List<IEX_SM_MD.Parameter> GetServiceMetadata(string serviceName, string implementationName, out string serviceDisplayName, out string implementationDisplayName)
        {
            Service serviceMetadata = GetSubsystemsMetadata()
                    .Where(service => service.ServiceName.Equals(serviceName)).SingleOrDefault();

            serviceDisplayName = serviceMetadata.DisplayName;

            Implementation implementationMetadata = serviceMetadata.Implementations
                    .Where(impl => impl.ImplementationName.Equals(implementationName)).SingleOrDefault();

            implementationDisplayName = implementationMetadata.DisplayName;

            return implementationMetadata.Parameters;
        }

        private IEXConfigurationSC.InstanceInfo GetInstance(IEXConfigurationSC.ServiceInfo serviceData)
        {
            return serviceData.Instances.SingleOrDefault();
        }

        private IEXConfigurationSC.InstanceInfo GetInstance(IEXConfigurationSC.ServiceInfo serviceData, string instanceName)
        {
            return serviceData.Instances.Where(instance => string.Equals(instance.Name, instanceName)).SingleOrDefault();
        }

        private IEXConfigurationSC.ServiceInfoCollection GetServicesData(int iexNumber)
        {
            return GetServerConfiguration(iexNumber).Services;
        }

        private IEXConfigurationSC.ServiceInfoCollection TryGettingServicesData(int iexNumber)
        {
            IEXConfigurationSC.ServiceInfoCollection services = null;

            try
            {
                services = GetServerConfiguration(iexNumber).Services;
            }
            catch (System.Configuration.ConfigurationErrorsException)
            {
                throw new IexServerConfigurationErrorsException(string.Format("Invalid Configuration of IEX {0} (its Configuration seems to be corrupted).", iexNumber));
            }

            if (services.Count == 0)
            {
                throw new IexServerKeyNotFoundException(string.Format("No services found in Configuration of IEX {0}.",iexNumber));
            }

            return services;
        }

        private IEXConfigurationSC.ServiceInfo GetServiceData(int iexNumber, string serviceName)
        {
            IEXConfigurationSC.ServiceInfoCollection services = TryGettingServicesData(iexNumber);

            return services.Where(service => service.Name.Equals(serviceName)).SingleOrDefault<IEXConfigurationSC.ServiceInfo>();
        }

        private IEXConfigurationSC.ServiceInfo TryGettingServiceData(int iexNumber, string serviceName)
        {
            IEXConfigurationSC.ServiceInfo serviceData = null;
            try
            {
                serviceData = GetServiceData(iexNumber, serviceName);
            }
            catch (InvalidOperationException)
            {
                //trying to add service display name from metadata for new requirement of more readable log message
                string serviceDisplayName;
                TryGettingServiceDisplayNameFromMetadata(serviceName, out serviceDisplayName);
                throw new IexServerArgumentException(string.Format("Multiple services having serviceName {1}{2} found in IEX {0} Configuration.", iexNumber, serviceName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName)));
            }

            if (serviceData == null)
            {
                //trying to add service display name from metadata for new requirement of more readable log message
                string serviceDisplayName;
                TryGettingServiceDisplayNameFromMetadata(serviceName, out serviceDisplayName);
                throw new IexServerArgumentException(string.Format("The serviceName {1}{2} cannot be found in IEX {0} Configuration.", iexNumber, serviceName, EncloseInParenthesesIfNotEmptyStr(serviceDisplayName)));
            }

            return serviceData;
        }

    }

    static class ExtensionHelpers
    {
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> source) { return source == null || !source.Any(); }
    }
}
