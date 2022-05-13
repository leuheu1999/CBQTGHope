using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface ILoggingService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/LogError")]
        ResultResponse<long> LogError(LogAddView model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/LogDebug")]
        ResultResponse<long> LogDebug(LogAddView model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/LogInformation")]
        ResultResponse<long> LogInformation(LogAddView model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/LogWarning")]
        ResultResponse<long> LogWarning(LogAddView model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DC/LogFatal")]
        ResultResponse<long> LogFatal(LogAddView model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DC/LogSystem_List")]
        ResultResponse<List<LogMap>> LogSystem_List(LogParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "DC/GetLogSystemById?id={id}")]
        ResultResponse<LogAdd> GetLogSystemById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "DC/LogSystem_DelByID?id={id}")]
        ResultResponse<int> LogSystem_DelByID(long id);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                            UriTemplate = "DC/LogSystem_DelLstID")]
        ResultResponse<int> LogSystem_DelLstID(List<long> lstid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                           UriTemplate = "DC/LogSystem_Trancate")]
        ResultResponse<int> LogSystem_Trancate();
    }
}
