using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;

namespace Logic
{
    public class UserManager
    {
        public Boolean RegisterUser(PlayerDTO player) 
        {         
            bool status = false;
            ValidateData validate = new ValidateData();

            if (player != null && validate.ValidationEmailFormat(player.Email) && validate.ValidationUsernameFormat(player.Username))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    if (!UserExist(player.Username, player.Email))
                    {
                        context.player.Add(new player() { email = player.Email, username = player.Username, password = player.Password, coins = 500, birthday = player.Birthday });
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
            ValidateData validate = new ValidateData();

            if (email != null && validate.ValidationEmailFormat(email))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var result = context.player.Where(x => x.email == email);
                    if (result.Count() > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public Boolean ValidationUsername(string username)
        {
            ValidateData validate = new ValidateData();

            if (username != null && validate.ValidationUsernameFormat(username))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var result = context.player.Where(x => x.username == username);
                    if (result.Count() > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        public bool UserExist(string username, string email)
        {
            if (username != null && email != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var result = context.player.Where(x => x.username == username || x.email == email);
                    if (result.Count() > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        public PlayerDTO AuthenticationLogin(string username, string password)
        {
            PlayerDTO playerDTO = new PlayerDTO()
            {
                IsActive = false
            };

            if (username != null && password != null)
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
            ValidateData validate = new ValidateData();

            if (emailPlayers != null && codeVerification != null && validate.ValidationEmailFormat(emailPlayers))
            {
                EmailStructure objLogic = new EmailStructure();
                string body = "Hello player I enclose your verification code " + codeVerification;
                
                return objLogic.SendMail(emailPlayers, " Verification Code ", body);      
            }

            return false;
        }

        public bool ChangePassword(string email, string password)
        {
            ValidateData validate = new ValidateData();

            if (email != null && password != null && validate.ValidationEmailFormat(email))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.email == email).FirstOrDefault();
                    if (player != null)
                    {
                        player.password = password;
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool ChangeUsername(string email, string username)
        {
            ValidateData validate = new ValidateData();

            if (email != null && validate.ValidationEmailFormat(email))
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.email == email).FirstOrDefault();
                    if (player != null)
                    {
                        player.username = username;
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public int CheckNumberOfFriends(string email)
        {
            if (email != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.email == email).FirstOrDefault();
                    if (player != null)
                    {
                        var counter = context.friendList.Where(x => x.idFriendList == email).Count();
                        return counter;
                    }
                }
            }
            return 0;
        }

        public bool AddFriend(string usernameSender, string usernameDestiner)
        {
            if (usernameSender != null && usernameDestiner != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var playerSender = context.player.Where(x => x.username == usernameSender).FirstOrDefault();
                    var playerDestiner = context.player.Where(x => x.username == usernameDestiner).FirstOrDefault();
                    if (playerSender != null && playerDestiner != null)
                    {
                        context.friendList.Add(new friendList() {idFriendList = playerSender.email, email = playerDestiner.email });
                        var result = context.SaveChanges();
                        if (result > 0) 
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool EliminateFriend(string usernamePlayer, string usernameFriend)
        {
            if (usernamePlayer != null && usernameFriend != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var playerSender = context.player.Where(x => x.username == usernamePlayer).FirstOrDefault();
                    var playerDestiner = context.player.Where(x => x.username == usernameFriend).FirstOrDefault();
                    if (playerSender != null && playerDestiner != null)
                    {
                        var player = context.friendList.Add(new friendList() { idFriendList = playerSender.email, email = playerDestiner.email });
                        context.friendList.Remove(player);

                        var result= context.SaveChanges();

                        if(result > 0)
                        {
                            return true;
                        }
                            
                        
                    }
                }
            }
            return false;
        }

        public bool AreFriends(string usernameSender, string usernameDestiner)
        {
            if (usernameSender != null && usernameDestiner != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var playerSender = context.player.Where(x => x.username == usernameSender).FirstOrDefault();
                    var playerDestiner = context.player.Where(x => x.username == usernameDestiner).FirstOrDefault();
                    if (playerSender != null && playerDestiner != null)
                    {
                        var result = context.friendList.Where(x => x.idFriendList == playerSender.email && x.email == playerDestiner.email).Count();
                        if (result > 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<PlayerDTO> GetFriendListDataBases(string username)
        {
            List<PlayerDTO> friendListUsers = new List<PlayerDTO>();
            if (username != null)
            {
                using (var context = new GameLoteriaDataBasesEntities())
                {
                    var player = context.player.Where(x => x.username == username).FirstOrDefault();
                    if (player != null)
                    {                        
                        var friendListDataBase = (from a in context.friendList where a.idFriendList == player.email select new PlayerDTO() { Email = a.email }).ToList();
                        friendListUsers = friendListDataBase;
                    }
                }
            }
            return friendListUsers;
        }



    }
}


