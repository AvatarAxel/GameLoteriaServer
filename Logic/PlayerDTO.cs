using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class PlayerDTO
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email  { get; set; }
        [DataMember]
        public System.DateTime Birthday { get; set; }
        [DataMember]
        public int Coin { get; set; }
    }
}