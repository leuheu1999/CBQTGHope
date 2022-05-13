using System;

namespace Business.Entities.Domain
{
    public class Response_1CuaMap<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }

    public class ResponseUser_1CuaMap<T>
    {
        public T Token { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
