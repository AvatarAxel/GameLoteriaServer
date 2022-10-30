using System.Runtime.Serialization;
using System.ServiceModel;

namespace Logic
{
    [DataContract]
    public class PlayerDTO
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email  { get; set; }
        [DataMember]
        public System.DateTime Birthday { get; set; }
        [DataMember]
        public int Coin { get; set; }
        public OperationContext Connection { get; set; }
    }
}