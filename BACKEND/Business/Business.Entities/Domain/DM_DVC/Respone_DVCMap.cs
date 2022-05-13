namespace Business.Entities.Domain
{
    public class Response_DVCMap<T>
    {
        public T data { get; set; }        
        public int error_code { get; set; }
        public string error_message { get; set; }
    }
}
