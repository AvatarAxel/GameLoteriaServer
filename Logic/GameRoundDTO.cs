using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Logic
{
    [DataContract]
    public class GameRoundDTO
    {
        [DataMember]
        public string VerificationCode { get; set; }
        public List<PlayerDTO> PlayerDTOs { get; set; }
        [DataMember]
        public int LimitPlayer { get; set; }
        [DataMember]
        public int Speed { get; set; }
        [DataMember]
        public bool PrivateGame { get; set; }
    }
}
