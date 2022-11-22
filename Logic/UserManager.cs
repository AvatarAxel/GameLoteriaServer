using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Runtime.Remoting.Contexts;
using System.Runtime.ConstrainedExecution;
using System.Net.NetworkInformation;


namespace Logic
{
    public class UserManager
    {
        public Boolean RegisterUser(PlayerDTO player) 
        {
            bool status = false;
            if (player == null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    if (!UserExist(player.Username, player.Email))
                    {
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
            return status;
        }

        public Boolean ValidationEmail(string email)
        {
            if (email == null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var result = context.player.Where(x => x.email == email);
                    if (result.Count() > 0)
                    {
                        return true;
                    }
                    return false;
                };
            }
            return false;
        }

        public Boolean ValidationUsername(string username)
        {
            if (username == null)
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
            return false;
        }
        public bool UserExist(string username, string email)
        {
            if (username == null || email == null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var result = context.player.Where(x => x.username == username || x.email == email);
                    if (result.Count() > 0)
                    {
                        return true;
                    }
                    return false;
                };
            }
            return false;
        }
        public PlayerDTO AuthenticationLogin(string username, string password)
        {
            PlayerDTO playerDTO = new PlayerDTO()
            {
                IsActive = false
            };

            if (username == null || password == null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var players = (from Player in context.player where Player.username == username && Player.password == password select Player);

                    if (players.Count() > 0)
                    {
                        playerDTO.Username = players.First().username;
                        playerDTO.Email = players.First().email;
                        playerDTO.IsActive = true;
                        playerDTO.Coin = players.First().coins;
                    }

                }
                return playerDTO;
            }
            return playerDTO;
        }

        public bool ReceiveEmail(string emailPlayers, string codeVerification)
        {
            if (emailPlayers == null)
            {
                EmailStructure objLogic = new EmailStructure();
                string body = "Hello player I enclose your verification code " + codeVerification;
                
                return objLogic.sendMail(emailPlayers, " Verification Code ", body);      
            }
            return false;
        }

        public bool ChangePassword(string email, string password)
        {
            bool status = false;
            if (email == null || password == null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.email == email).FirstOrDefault();
                    if (player != null)
                    {
                        player.password = password;
                        if (context.SaveChanges() > 0)
                        {
                            status = true;
                        }
                    }
                }
            }
            return status;
        }

    }
}


