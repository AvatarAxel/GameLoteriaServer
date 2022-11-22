using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Comunication;
using System.Xml.Linq;
using Logic;
using System.Threading;
using Microsoft.Win32;
using System.ServiceModel.Channels;
using System.Security;
using System.Text.RegularExpressions;

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ServicesExposed : IAuthenticationService
    {
        public PlayerDTO AuthenticationLogin(string name, string password)
        {
            UserManager authentication = new UserManager();
            PlayerDTO playerDTO = authentication.AuthenticationLogin(name, password);
            return playerDTO;
        }
    }

  
    public partial class ServicesExposed : IChatService
    {
        List<GameRoundDTO> lobbyChat = new List<GameRoundDTO>();
        public void CreateChat(string verificationCode)
        {
            List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
            GameRoundDTO gameRoundDTO = new GameRoundDTO()
            {
                VerificationCode = verificationCode,
                playerDTOs = playerDTOs

            };
            lobbyChat.Add(gameRoundDTO);
        }

        public void JoinChat(string username, string verificationCode)
        {
            var ChatNew = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (ChatNew != null)
            {
                PlayerDTO players = new PlayerDTO()
                {
                    Username = username,
                };                
                players.Connection = OperationContext.Current;
                ChatNew.playerDTOs.Add(players);
                SendMessage("Join chat", username, verificationCode);

            }
        }

        public void SendMessage(string message, string userChat, string verificationCode) 
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (ChatExisting != null)
            {
                for (int i = 0; i < ChatExisting.playerDTOs.Count; i++)
                {
                    try
                    {
                        ChatExisting.playerDTOs[i].Connection.GetCallbackChannel<IChatServiceCallBack>().ReciveMessage(userChat, message);
                    }
                    catch (CommunicationObjectAbortedException)
                    {
                        ChatExisting.playerDTOs.Remove(ChatExisting.playerDTOs[i]);
                    }
                }
            }
        }
        public void ExitChat(string userName, string verificationCode)
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if(ChatExisting != null)
            {
                var player = ChatExisting.playerDTOs.FirstOrDefault(iteration => iteration.Username == userName);
                if(player != null)
                {
                    player.Connection.GetCallbackChannel<IChatServiceCallBack>().ReciveMessage(player.Username, "Exit Chat");
                    ChatExisting.playerDTOs.Remove(player);
                    SendMessage("Exited the chat", userName, verificationCode);
                }
            }
        }
        public void DeleteChat(string verificationCode)
        {
            var ChatExisting = lobbyChat.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (ChatExisting != null)
            {
                lobbyChat.Remove(ChatExisting);
            }
        }
    }
    public partial class ServicesExposed : IChangePasswordService
    {
        public bool ChangePassword(string email, string password)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ChangePassword(email, password);
            return status;
        }
    }
    public partial class ServicesExposed : IEmailService
    {
        public string ValidationEmail(string email)
        {
            UserManager userManager = new UserManager();
            string verificationCode = userManager.ReceiveEmail(email);
            return verificationCode;
        }
    }
    public partial class ServicesExposed : IUserRegistrationService 
    {
        public bool RegistrerUserDataBase(PlayerDTO player)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.RegisterUser(player);
            return status;
        }

        public bool ValidationEmailDataBase(string email)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ValidationEmail(email);
            return status;
        }

        public bool ValidationUsernameDataBase(string username)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ValidationUsername(username);
            return status;
        }   
    }
    public partial class ServicesExposed : IJoinGameService
    {
        List<GameRoundDTO> gameRoundDTOs = new List<GameRoundDTO>();
        public void CreateGame(string verificationCode, int limitPlayers)
        {
            List<PlayerDTO> ListPlayerDTOs = new List<PlayerDTO>();
            GameRoundDTO gameRoundDTO = new GameRoundDTO()
            {
                VerificationCode = verificationCode,
                LimitPlayer = limitPlayers,
                playerDTOs = ListPlayerDTOs

            };
            gameRoundDTOs.Add(gameRoundDTO);
        }

        public void EliminateGame(string verificationCode)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                gameRoundDTOs.Remove(game);       
            }
        }

        public void ExitGame(string userName, string verificationCode)
        {
            var lobby = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                var player = lobby.playerDTOs.FirstOrDefault(iteration => iteration.Username == userName);
                if (player != null)
                {
                    lobby.playerDTOs.Remove(player);
                }
            }
        }

        public void JoinGame(string username, string verificationCode)
        {
            var newConnection = OperationContext.Current;
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                PlayerDTO player = new PlayerDTO()
                {
                    Username = username,
                };
                player.Connection = newConnection;
                game.playerDTOs.Add(player);
                newConnection.GetCallbackChannel<IJoinGameServiceCallBack>().ResponseTotalPlayers(game.playerDTOs.Count);
            }
        }

        public void SendWinner(string username, string verificationCode)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                var player = game.playerDTOs.FirstOrDefault(iteration => iteration.Username == username);
                try
                {
                    var conection = player.Connection.GetCallbackChannel<IJoinGameServiceCallBack>();
                    conection.ReciveWinner(username);
                }
                catch (CommunicationObjectAbortedException)
                {
                    game.playerDTOs.Remove(player);                
                }
            }
        }

        public void SendNextHostGame(string verificationCode) 
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                foreach (PlayerDTO user in game.playerDTOs)
                {
                    if (!Regex.IsMatch(user.Username, "Invitado"))
                    {
                        user.Connection.GetCallbackChannel<IJoinGameServiceCallBack>().SendNextHostGameResponse(true);
                        return;
                    }
                }
                EliminateGame(verificationCode);
            }
        }

        public bool ResponseCodeExist(string verificationCode)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                return true;
            }
            return false;
        }

        public bool ResponseCompleteLobby(string verificationCode)
        {
            var game = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (game != null)
            {
                if (game.playerDTOs.Count >= game.LimitPlayer)
                    return true;
            }
            return false;
        }
    }
}
