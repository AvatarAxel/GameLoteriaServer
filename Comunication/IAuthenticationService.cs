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

        [OperationContract(IsOneWay = true)]
        void RegistrerUserBD(PlayerDTO player);

        [OperationContract(IsOneWay = true)]
        void ValidationEmail(String email);
    }

    
    [ServiceContract]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseAuthenticated(PlayerDTO playerDTO);

        [OperationContract(IsOneWay = true)]
        void ResponseEmail(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ResponseRegister(bool status);
    }
}

