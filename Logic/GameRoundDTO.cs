using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class GameRoundDTO
    {
        public string VerificationCode { get; set; }
        public List<PlayerDTO> playerDTOs { get; set; }
    }
}
