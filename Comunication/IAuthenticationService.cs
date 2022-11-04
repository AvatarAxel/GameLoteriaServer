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
    [ServiceContract(CallbackContract = typeof(IAuthenticationServiceCallBack))]
    public interface IAuthenticationService
    {
        [OperationContract(IsOneWay = true)]
        void AuthenticationLogin(String name, String password);
    }

    
    [ServiceContract]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseAuthenticated(PlayerDTO playerDTO);
    }
}

