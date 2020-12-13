using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Server.Monitor.Host
{
    using IEX.Utilities;
    using System.ServiceModel;
    //using IEX.Server.Monitor.WebServices;
    using IEX_SM_MD = IEX.Server.ServiceManagement.MetaData;
    using IEX.TestFramework.Engine.Service;
    using IEX.TestFramework.Engine;
    //using IEX.ManagementServer.WebServices.Execution;

    class Manager
    {
        static readonly IMonitoringService _monitoring_service = new MonitoringService();
        static readonly IConfigurationService _configuration_service = new ConfigurationService();
        static readonly IEnvironmentService _environment_service = new EnvironmentService();
        //static readonly ILoggingService _logging_service = new LoggingService();        
        static AppDomain testExecutionAppDomain = null;
        //static ExecutionServiceHost testExecutionServiceHost = null;

        static readonly List<ServiceHost> _hosts = new List<ServiceHost>();

        const string base_address_monitoring = "http://localhost:8732/IEX.Server.Monitor/MonitoringService/";
        const string base_address_configuration = "http://localhost:8732/IEX.Server.Monitor/ConfigurationService/";
        const string base_address_environment = "http://localhost:8732/IEX.Server.Monitor/EnvironmentService/";
        const string base_address_logging = "http://localhost:8732/IEX.Server.Monitor/LoggingService/";
        const string base_address_testExecution = "http://localhost:8732/IEX.Server.Monitor/TestExecutionService/";

        private readonly System.Threading.Timer _timer_refresh_providers;
        public Manager()
        {
            _timer_refresh_providers = new System.Threading.Timer(new System.Threading.TimerCallback(RefreshProvidersElapsed));
        }

        private void RefreshProvidersElapsed(object obj)
        {
            _timer_refresh_providers.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            FillProviders();
        }

        public void StartHosts()
        {
            Tools.WriteLog("StartHosts: start", Tracer.TraceLevel.API_ENTER);
            Tools.WriteLog("Current Directory: " + System.IO.Directory.GetCurrentDirectory());
            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


            //foreach (ServiceHost host in _hosts)
            //    host.Close();

            StopHosts();

            try
            {
                ServiceHost monitoring_host = new ServiceHost(_monitoring_service, new Uri(base_address_monitoring));
                _hosts.Add(monitoring_host);
                //start listening for requests
                _hosts[_hosts.Count - 1].Open();
                _monitoring_service.Start();

                ServiceHost configuration_host = new ServiceHost(_configuration_service, new Uri(base_address_configuration));
                _hosts.Add(configuration_host);
                //start listening for requests
                _hosts[_hosts.Count - 1].Open();

                ServiceHost environment_host = new ServiceHost(_environment_service, new Uri(base_address_environment));
                _hosts.Add(environment_host);
                //start listening for requests
                _hosts[_hosts.Count - 1].Open();

                //ServiceHost logging_host = new ServiceHost(_logging_service, new Uri(base_address_logging));
                //_hosts.Add(logging_host);
                ////start listening for requests
                //_hosts[_hosts.Count - 1].Open();

                //ExecutionServiceHost.CreateAppDomainAndServiceHost(out testExecutionAppDomain, out testExecutionServiceHost);
                //testExecutionServiceHost.Open(base_address_testExecution);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Tools.WriteLog(e.Message, Tracer.TraceLevel.ERROR);
            }

            Tools.WriteLog("StartHosts: end", Tracer.TraceLevel.API_EXIT);
#if IEX_DEBUG
            foreach (ServiceHost service_host in _hosts)
            {
                string description = IEX.Utilities.WcfHelper.Description(service_host);
                Console.WriteLine(description);
            }
            //Console.WriteLine(testExecutionServiceHost.GetServiceDescription());
#endif
        }

        public void StopHosts()
        {
            Tools.WriteLog("StopHosts: start", Tracer.TraceLevel.API_ENTER);
            foreach (ServiceHost host in _hosts)
            {
                if (host.State != CommunicationState.Faulted)
                    host.Close();
            }

            //ExecutionServiceHost.CleanupAppDomanAndServiceHost(testExecutionAppDomain, testExecutionServiceHost);

            _hosts.Clear();
            Tools.WriteLog("StopHosts: end", Tracer.TraceLevel.API_EXIT);
        }

        public bool StartIEX(int iex_number)
        {
            ServerInfo server_info;
            return _monitoring_service.StartIEX(iex_number, true, out server_info);
        }

        public void StopIEX(int iex_number)
        {
            _monitoring_service.StopIEX(iex_number);
        }

        public bool RestartIEX(int iex_number)
        {
            ServerInfo server_info;
            return _monitoring_service.RestartIEX(iex_number, true, out server_info);
        }

        public delegate void FillProvidersHandler();
        public void FillProviders(int start_time)
        {
            _timer_refresh_providers.Change(start_time, System.Threading.Timeout.Infinite);
        }

        public void FillProviders()
        {
            Console.WriteLine(DateTime.Now.ToString() + " Started loading providers...");
            Tools.WriteLog("FillProviders: start", Tracer.TraceLevel.API_ENTER);
            Tools.WriteLog(string.Format("Total Memory before {0} FillProviders", GC.GetTotalMemory(false)));
            try
            {
                IEX_SM_MD.ServiceCollection subsytems_metadata = _configuration_service.GetSubsystemsMetadata();
                foreach (IEX_SM_MD.Service service in subsytems_metadata)
                {
                    if (service.Implementations != null)
                    {
                        foreach (IEX_SM_MD.Implementation implementation in service.Implementations)
                        {
                            bool message = false;
                            if (implementation.Parameters != null)
                            {
                                foreach (IEX_SM_MD.Parameter parameter in implementation.Parameters)
                                {
                                    if (!string.IsNullOrEmpty(parameter.ValuesProviders) && !parameter.ValuesProviders.StartsWith("IEX.Server.ServiceManagement.ServiceDependency,"))
                                    {
                                        if (message == false)
                                        {
                                            Console.WriteLine(string.Format("{0} Fill '{1}' providers.", DateTime.Now.ToString(), implementation.DisplayName));
                                            message = true;
                                        }
                                        _environment_service.GetValues(parameter.ValuesProviders);
                                    }
                                    if (!string.IsNullOrEmpty(parameter.RichInfoProviders))
                                    {
                                        if (message == false)
                                        {
                                            Console.WriteLine(string.Format("{0} Fill '{1}' providers.", DateTime.Now.ToString(), implementation.DisplayName));
                                            message = true;
                                        }
                                        _environment_service.GetValues(parameter.RichInfoProviders);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Tools.WriteLog(e.Message, Tracer.TraceLevel.ERROR);
            }
            Console.WriteLine(DateTime.Now.ToString() + " Finished loading providers.");
            Console.WriteLine("");
            Console.WriteLine(DateTime.Now.ToString() + " Type 'Exit' to shutdown");

            Tools.WriteLog("FillProviders: end", Tracer.TraceLevel.API_EXIT);
        }
    }
}
