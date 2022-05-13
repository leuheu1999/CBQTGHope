namespace Core.Common.Utilities
{
    public class ResultResponse<T>
    {
        public string status { get; set; }
        public int StatusCode { get; set; }
        public ResultResponse()
        {
            
        }
        public ResultResponse(ResponseModel response,T result)
        {
            status = response.status;
            StatusCode = response.StatusCode;
            description = response.description;
            resultType = response.resultType;
            throwException = response.throwException;
            resultObject = result;
        }
        public T resultObject { get; set; }
        public string description { get; set; }
        public string resultType { get; set; }
        public bool throwException { get; set; }
    }
}
