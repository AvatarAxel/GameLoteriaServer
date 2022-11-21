using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text; 
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        PlayerDTO AuthenticationLogin(String name, String password);
    }
}

