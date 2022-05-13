using System.Runtime.Serialization;

namespace Business.Entities
{
    [DataContract]
    public class BanIPBindingModel
    {
       
        [DataMember]
        public string IPAdress { get; set; }
        [DataMember]
        public int? WrongNumber { get; set; }
    }
}
