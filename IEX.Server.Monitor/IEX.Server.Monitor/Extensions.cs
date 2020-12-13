using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IEX.Server.Monitor
{
    public static class Extensions
    {
        public static ICollection<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance> ToPerformanceCounters(this Dictionary<string, string> info)
        {
            var result = new List<IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance>();

            if (info != null)
            {
                foreach (var i in info)
                {
                    if (i.Key == "% Processor Time")
                    {
                        IEX.ManagementServer.Model.Monitoring.PerformanceCounter cpuPerformanceCounter = new ManagementServer.Model.Monitoring.PerformanceCounter
                        {
                            Name = "ComputerCPU",
                            Threshold = 80,
                          
                        };

                        IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance cpuCounterInstance = new ManagementServer.Model.Monitoring.PerformanceCounterInstance
                        {
                            PerformanceCounter = cpuPerformanceCounter,
                            Value = float.Parse(i.Value, CultureInfo.InvariantCulture.NumberFormat)

                        };
                        result.Add(cpuCounterInstance);
                    }
                    else if (i.Key == "Working Set")
                    {

                        IEX.ManagementServer.Model.Monitoring.PerformanceCounter memoryPerformanceCounter = new ManagementServer.Model.Monitoring.PerformanceCounter
                        {
                            Name = "Working Set",
                            Threshold = 80,
                          
                        };

                        IEX.ManagementServer.Model.Monitoring.PerformanceCounterInstance memoryCounterInstance = new ManagementServer.Model.Monitoring.PerformanceCounterInstance
                        {
                            PerformanceCounter = memoryPerformanceCounter,
                            Value = float.Parse(i.Value, CultureInfo.InvariantCulture.NumberFormat)

                        };

                        result.Add(memoryCounterInstance);
                    }
                }
            }

            return result;
        }
    }
}
