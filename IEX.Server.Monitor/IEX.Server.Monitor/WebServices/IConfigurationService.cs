namespace IEX.Server.Monitor
{
    using System.ServiceModel;
    using System;
    using System.Runtime.Serialization;
    using IEX_SM_MD = IEX.Server.ServiceManagement.MetaData;
    using IEXConfigurationSC = IEX.Server.Monitor.Configuration.ServerConfiguration;
    using IEX.Server.WebServices.Core;
    using System.ServiceModel.Web;

    [ServiceContract(Namespace = "http://IEX.Server.Monitor/ConfigurationService")]
    public interface IConfigurationService
    {
        [OperationContract]
        [FaultContract(typeof(IEX.Server.WebServices.Core.IexServerConfigurationFault))]
        [IEX.Utilities.ServiceBehavior.MethodsErrorItemBehavior(typeof(System.Configuration.ConfigurationErrorsException), typeof(IEX.Server.WebServices.Core.IexServerConfigurationFault))]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEXConfigurationSC.IexServerConfiguration GetServerConfiguration(int id);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SetServerConfiguration(int iex_number, IEXConfigurationSC.IexServerConfiguration data);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEX_SM_MD.ServiceCollection GetSubsystemsMetadata();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string[] GetDefaultValues(int id, string implementation_name);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        int[] GetConfiguratedIdsServers();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        int[] GetAutomaticConfiguratedIdsServers();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEXConfigurationSC.IexServerConfiguration ConfigurationDeserialize(string data);

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string ConfigurationSerialize(IEXConfigurationSC.IexServerConfiguration data);

        [OperationContract]
        [FaultContract(typeof(IexServerArgumentFault))]
        [FaultContract(typeof(IexServerConfigurationFault))]
        [FaultContract(typeof(IexServerKeyNotFoundFault))]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetConfigurationParameterValue(int iexNumber, string serviceName, string parameterName, string implementationName = "", string instanceName = "");
    }
}
