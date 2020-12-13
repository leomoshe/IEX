using System.ServiceModel;
using System.ServiceModel.Web;
using IEX.ManagementServer.Model.Resources;

namespace IEX.Server.Monitor
{
    [ServiceContract]
    public interface IEnvironmentService
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string[] GetValues(string provider_assembly_qualified_name);

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        void ResetValues();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        string GetVersion();

        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.Wrapped)]
        Computer GetComputerDetails();
    }
}
