using System;
using System.Data.SqlClient;
namespace Core.Common.Utilities
{
    public class ResponseModel
    {
        public string status { get; set; }
        public int StatusCode { get; set; }
        public string resultType { get; set; }
        public bool throwException { get; set; }
        public string description { get; set; }
        public ResponseModel()
        {
            StatusCode = (int)System.Net.HttpStatusCode.OK;
            status = "SUCCESS";
            resultType = "JSON";
            throwException = false;
        }
        public ResponseModel(Exception ex)
        {
            throwException = true;
            status = "FAIL";
            if (ex is TimeoutException)
            {
                StatusCode = (int)System.Net.HttpStatusCode.RequestTimeout;
                description = "Time out";
            }
            else if (ex is SqlException)
            {
                StatusCode = -1;
                description = "Lỗi SQL";
            }
            else
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                description = "Lỗi server không xác định";
            }
        }
        public ResponseModel(string message)
        {
            throwException = true;
            status = "FAIL";
            StatusCode = (int)System.Net.HttpStatusCode.OK;
            description = message;
        }
        public ResponseModel(string message, int statuserror)
        {
            throwException = true;
            status = "FAIL";
            StatusCode = statuserror;
            description = message;
        }
    }
}
