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
        public void IsAuthenticated(string name, string password)
        {
            Authentication authentication = new Authentication();
            bool status = authentication.IsAuthenticated(name, password);
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().NombreEjemplo(status);
        }

        public void RegistrerUserBD(PlayerDTO player)
        {
            UserManager userManager = new UserManager();
            bool status = userManager.RegisterUser(player);
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().NombreEjemplo(status);
        }

        public void ReponseAuthenticated(string username, string email, string password01)
        {
            Authentication authentication = new Authentication();
            bool status = authentication.ReponseAuthenticated(username, email, password01);
            //LLama al cliente y mediante el metodo CallBack te mando la inf
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().NombreEjemplo(status);
        }


    }

}
