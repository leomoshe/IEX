using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEX.Lab.Client.Extensions
{
    public static class Extensions
    {
        public static Dictionary<string, string> ToPerformanceCountersDictionary(this IEX.Server.Monitor.Client.MonitoringServiceReference.PerformanceCounterInstance[] performanceCounters)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (performanceCounters == null)
                return result;

            foreach (var counter in performanceCounters)
            {
                if (counter.PerformanceCounter.Name=="ComputerCPU")
                {
                    string cpuID = "% Processor Time";
                    string cpuValue = counter.Value.ToString();
                    result.Add(cpuID, cpuValue);
                }
                else if (counter.PerformanceCounter.Name == "Working Set")
                {
                    string memoryId = "Working Set";
                    string memoryValue = counter.Value.ToString();
                    result.Add(memoryId, memoryValue);
                }
            }


            return result;
        }
    }
}
