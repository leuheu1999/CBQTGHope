using System.Collections.Generic;
namespace Core.Common.Utilities
{
    public class ResultResponse<T>
    {
        public string status { get; set; }
        public int StatusCode { get; set; }
        public ResultResponse()
        {
            
        }
        public T resultObject { get; set; }
        public string description { get; set; }
        public string resultType { get; set; }
        public bool throwException { get; set; }
    }

    public class ResultResponseTest<T>
    {
        public T set_attributes { get; set; }
        
    }
}
