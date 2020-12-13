using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEX.Utilities;
using System.ServiceModel;
using IEX.ManagementServer.Model.Resources;
using System.Management;

namespace IEX.Server.Monitor
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class EnvironmentService: IEnvironmentService
    {
        //private System.Threading.Timer _refresh_timer;
        //public EnvironmentService()
        //{
        //    int interval = (int)new TimeSpan(0, 0, 10).TotalMilliseconds;
        //    _refresh_timer = new System.Threading.Timer(OnElapsed, null, interval, interval);
        //}

        //private void OnElapsed(object sender)
        //{
        //    ResetValues();
        //}

        private Dictionary<string, string[]> _lookups = new Dictionary<string, string[]>();
        private string[] Values(string provider_assembly_qualified_name)
        {
            Tracer.Write(Tracer.TraceLevel.DEBUG, "Input: " + provider_assembly_qualified_name);
            string[] result = null;
            string parameters = null;
            if (provider_assembly_qualified_name.EndsWith("}"))
            {
                int start = provider_assembly_qualified_name.LastIndexOf('{');
                parameters = provider_assembly_qualified_name.Substring(start+1, provider_assembly_qualified_name.Length-start-2);
                provider_assembly_qualified_name = provider_assembly_qualified_name.Substring(0, start);
            }
            Type type = Type.GetType(provider_assembly_qualified_name);
            if (type != null)
            {
                object provider = null;
                if (parameters != null)
                    provider = Activator.CreateInstance(type, parameters);
                else
                    provider = Activator.CreateInstance(type);
                var valuesProperty = provider.GetType().GetProperty("Values");
                if (valuesProperty != null)
                {
                    var values = valuesProperty.GetValue(provider, null) as IEnumerable;
                    if (values != null)
                    {
                        result = (values.Cast<object>().Select(item => item.ToString())).ToArray();
                    }
                }
            }
            if (result == null)
                result = new string[0];
            Tracer.Write(Tracer.TraceLevel.DEBUG, "Output : " + string.Join(",", result));
            return result;
        }

        #region IEnvironmentService
        public string[] GetValues(string provider_assembly_qualified_name)
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered", new object[] { provider_assembly_qualified_name });
            if (string.IsNullOrEmpty(provider_assembly_qualified_name))
                return null;
            lock (_lookups)
            {
                if (!_lookups.ContainsKey(provider_assembly_qualified_name))
                    _lookups.Add(provider_assembly_qualified_name, Values(provider_assembly_qualified_name));
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + string.Join(",", _lookups[provider_assembly_qualified_name]));
            return _lookups[provider_assembly_qualified_name];
        }

        public void ResetValues()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            Console.WriteLine(DateTime.Now.ToString() + " Started refreshing providers values...");
            string[] keys = _lookups.Keys.ToArray();
            Dictionary<string, string[]> lookups = new Dictionary<string, string[]>();
            foreach (string provider_assembly_qualified_name in keys)
            {
                string[] values = Values(provider_assembly_qualified_name);
                lookups[provider_assembly_qualified_name] = values;
            }
            lock (_lookups)
            {
                _lookups = lookups;
            }
            Console.WriteLine(DateTime.Now.ToString() + " Finished refreshing providers values.");
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting");
        }

        public string[] GetProviders()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            string[] result = _lookups.Keys.ToArray();
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + string.Join(",", result));
            return result;
        }

        public string GetVersion()
        {
            Tracer.Write(Tracer.TraceLevel.API_ENTER, "entered");
            string result = string.Empty;
            System.Reflection.Assembly assembly = null;
            System.Reflection.Assembly[] assemblies = System.Threading.Thread.GetDomain().GetAssemblies();
            int index = Array.FindIndex(assemblies, item => item.FullName.StartsWith("IEX.Server.ServiceManagement"));
            if (index != -1)
                assembly = assemblies[index];
            else
                assembly = IEX.Utilities.Library.LoadAssembly(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "IEX.Server.ServiceManagement.dll"));
            if (assembly != null)
            {
                string[] items = assembly.FullName.Split(',');
                string version = Array.Find(items, item => item.Trim().StartsWith("Version="));
                if (version != null)
                    result = version.Trim().Split('=')[1];
            }
            Tracer.Write(Tracer.TraceLevel.API_EXIT, "exiting. result is: " + result);
            return result;
        }

        public IEX.ManagementServer.Model.Resources.Computer GetComputerDetails()
        {
            bool is64BitOS = Environment.Is64BitOperatingSystem;

            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            string osFriendlyName = name != null ? name.ToString() : "Unknown";

            Computer result = new Computer();
            result.System = is64BitOS ? "64 bit" : "32 bit";
            result.OS = osFriendlyName;
            result.IEXInstallationFolder = IEX.Utilities.IEXConfiguration.GetIexInstallationFolder();
            result.HostName = Environment.MachineName;
            return result;
        }
        #endregion IEnvironmentService
    }
}
