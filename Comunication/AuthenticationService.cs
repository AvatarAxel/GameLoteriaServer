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

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class AuthenticationService : IAuthenticationService
    {
        public void AuthenticationLogin(string name, string password)
        {
            UserManager authentication = new UserManager();
            bool status = authentication.AuthenticationLogin(name, password);
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().ResponseAuthenticated(status);
        }

        public void RegistrerUserBD(PlayerDTO player)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.RegisterUser(player);
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().ResponseAuthenticated(status);
        }
    }
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public partial class Chat : IChat
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
            foreach (var user in playerDTOs)
            {
                var connetion = user.Connection.GetCallbackChannel<IChatServiceCallBack>();
                connetion.ReciveMessage(userChat, message);
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
}
