using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserManager
    {
        public Boolean RegisterUser(PlayerDTO player) 
        {
            bool status = false;
            using (var context = new GameLoteriaDataBasesEntities())
            {
                var newPlayerDB = context.player.Add(new player() { email = player.Email, username = player.username, password = player.Password, coins = 500, birthday = player.Birthday });
                var resultado = context.SaveChanges();
                if (resultado > 0)
                {
                    status = true;
                }
            }
            return status;
        }
    }
}
