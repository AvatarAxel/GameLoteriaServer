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

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, InstanceContextMode = InstanceContextMode.Single)]
    public partial class ServicesExposed : IAuthenticationService
    {
        public void AuthenticationLogin(string name, string password)
        {
            UserManager authentication = new UserManager();
            PlayerDTO playerDTO = authentication.AuthenticationLogin(name, password); //InvalidOperationException:
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().ResponseAuthenticated(playerDTO);
        }
    }

  
    public partial class ServicesExposed : IChatService
    {
        List<PlayerDTO> playerDTOs = new List<PlayerDTO>();
        public void JoinChat(string username)
        {
            PlayerDTO player = new PlayerDTO()
            {
                Username = username,
            };
            var connection = OperationContext.Current;
            player.Connection = connection;
            playerDTOs.Add(player);
        }

        public void SendMessage(string message, string userChat)
        {
            for (int i = 0; i < playerDTOs.Count; i++) 
            {
                try
                {                     
                    var connetion = playerDTOs[i].Connection.GetCallbackChannel<IChatServiceCallBack>();
                    connetion.ReciveMessage(userChat, message);
                }
                catch (CommunicationObjectAbortedException)
                {
                    playerDTOs.Remove(playerDTOs[i]);
                }
            }
        }
        public void ExitChat(string userName)
        {
            var player = playerDTOs.FirstOrDefault(iteration => iteration.Username == userName);
            if (player != null)
            {
                playerDTOs.Remove(player);
            }
        }
    }
    public partial class ServicesExposed : IChangePasswordService
    {
        public void ChangePassword(string email, string password)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.ChangePassword(email, password);
            OperationContext.Current.GetCallbackChannel<IChangePasswordServiceCallBack>().ResponseChangePassword(status);
        }
    }
    public partial class ServicesExposed : IEmailService
    {
        public void ValidationEmail(string email)
        {
            UserManager userManager = new UserManager();
            string verificationCode = userManager.ReceiveEmail(email);
            OperationContext.Current.GetCallbackChannel<IEmailServiceCallBack>().ResponseEmail(verificationCode);
        }
    }
    public partial class ServicesExposed : IUserRegistrationService 
    {
        public void RegistrerUserBD(PlayerDTO player)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.RegisterUser(player);
            OperationContext.Current.GetCallbackChannel<IUserRegistrationServiceCallBack>().ResponseRegister(status);
        }
    }
    public partial class ServicesExposed : IJoinGameService
    {
        List<GameRoundDTO> gameRoundDTOs = new List<GameRoundDTO>();


        public void CreateGame(string verificationCode)
        {
            GameRoundDTO gameRoundDTO = new GameRoundDTO() 
            { 
                VerificationCode = verificationCode
            };
            gameRoundDTOs.Add(gameRoundDTO);
        }

        public void ExitGame(string userName, string verificationCode)
        {
            var lobby = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                var player = lobby.Lobby.FirstOrDefault(iteration => iteration.Username == userName);
                if (player != null)
                {
                    lobby.Lobby.Remove(player);
                }
            }
        }

        public void JoinGame(string username, string verificationCode)
        {
            PlayerDTO player = new PlayerDTO()
            {
                Username = username,
            };
            bool status = false;
            var newConnection = OperationContext.Current;
            player.Connection = newConnection;
            var lobby = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                lobby.Lobby.Add(player);
                status = true;
            }
            status = lobby.VerificationCode.Equals(verificationCode);
            var connetion = player.Connection.GetCallbackChannel<IJoinGameServiceCallBack>();
            connetion.CodeExist(status);
        }

        public void SendWinner(string username, string verificationCode)
        {
            var lobby = gameRoundDTOs.FirstOrDefault(iteration => iteration.VerificationCode == verificationCode);
            if (lobby != null)
            {
                var player = lobby.Lobby.FirstOrDefault(iteration => iteration.Username == username);
                try
                {
                    var conection = player.Connection.GetCallbackChannel<IJoinGameServiceCallBack>();
                    conection.ReciveWinner(username);
                }
                catch (CommunicationObjectAbortedException)
                {
                    lobby.Lobby.Remove(player);                
                }
            }
        }
    }
}
