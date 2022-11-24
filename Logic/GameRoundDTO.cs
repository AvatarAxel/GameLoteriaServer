using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    [DataContract]
    public class GameRoundDTO
    {
        [DataMember]
        public string VerificationCode { get; set; }
        public List<PlayerDTO> playerDTOs { get; set; }
        [DataMember]
        public int LimitPlayer { get; set; }
        [DataMember]
        public int Speed { get; set; }
    }
}
