using System;
using Logic;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Comunication;

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
    }
    
}
