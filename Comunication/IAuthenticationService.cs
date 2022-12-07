using Logic;
using System;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    public interface IAuthenticationService
    {
        [OperationContract]
        PlayerDTO AuthenticationLogin(String name, String password);
    }
}

