using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IAuthService
    {

        #region login
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                            UriTemplate = "Auth/ValidateUser")]
        ResultResponse<TokenObject> ValidateUser(LoginParam model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                           UriTemplate = "Auth/ValidateToken")]
        ResultResponse<bool> ValidateToken(TokenObject model);
        #endregion
    }
}
