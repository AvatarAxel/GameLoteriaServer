using System;
//using Logic;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Comunication;
using System.Xml.Linq;

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public partial class AuthenticationService : IAuthenticationService
    {
        public void IsAuthenticated(string name, string password)
        {
            Authentication authentication = new Authentication();
            bool status = authentication.IsAuthenticated(name, password);
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().ReponseAuthenticated(status);
        }

        public void ReponseAuthenticated(string username, string email, string password01)
        {
            Authentication authentication = new Authentication();
            bool status = authentication.ReponseAuthenticated(username, email, password01);
            //LLama al cliente y mediante el metodo CallBack te mando la inf
            OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallBack>().ReponseAuthenticated(status);
        }
    }


}
