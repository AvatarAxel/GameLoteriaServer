using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Runtime.ConstrainedExecution;


namespace Logic
{
    public class UserManager
    {
        public Boolean RegisterUser(PlayerDTO player) 
        {
            bool status = false;
            using (var context = new GameLoteriaDataBasesEntities())
            {
                if (!UserExist(player.Username, player.Email)) {
                    var newPlayerDB = context.player.Add(new player() { email = player.Email, username = player.Username, password = player.Password, coins = 500, birthday = player.Birthday });
                    var resultado = context.SaveChanges();
                    if (resultado > 0)
                    {
                        status = true;
                    }
                }
            }
            return status;
        }
        public bool UserExist(string username, string email)
        {
            using (var context = new GameLoteriaDataBasesEntities())
            {
                var result = context.player.Where(x => x.username == username);
                if (result.Count() > 0) 
                {
                    return true;
                }
                return false;
            };
        }
        public PlayerDTO AuthenticationLogin(string username, string password)
        {
            PlayerDTO playerDTO = new PlayerDTO()
            {
                IsActive = false
            };
            using (var context = new GameLoteriaDataBasesEntities())
            {
                var players = (from Player in context.player where Player.username == username && Player.password == password select Player);
                
                if(players.Count() > 0)
                {
                    playerDTO.Username = players.First().username; 
                    playerDTO.Email = players.First().email;
                    playerDTO.IsActive = true;
                    playerDTO.Coin = players.First().coins;
                }
              
            }
            return playerDTO;
        }

        public string ReceiveEmail(string EmailPlayers)
        {
            var random = new Random();
            var value = random.Next(0, 10000);

            string verificationCode = value.ToString();

            EmailStructure objLogic = new EmailStructure();
            string body = "Hello player I enclose your verification code " + verificationCode;
            objLogic.sendMail(EmailPlayers, " Verification Code ", body);

            return verificationCode;
        }

        public bool ChangePassword(string email, string password)
         {
             bool status = true;
             using (var context = new GameLoteriaDataBasesEntities())
             {
                var player = context.player.Where(x => x.email == email).FirstOrDefault();
                player.password = password;
                context.SaveChanges();
             }
            return status;
         }

    }
}


