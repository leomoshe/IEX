namespace IEX.Server.Monitor
{
    using System;
    using System.ServiceModel;
    using System.Collections.Generic;
    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IPinger
    {
        [OperationContract]
        ICollection<ManagementServer.Model.Monitoring.PerformanceCounterInstance> Ping();
    }
}
