using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text; 
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IAuthenticationServiceCallBack))]
    public interface IAuthenticationService
    {
        [OperationContract(IsOneWay = true)]
        void IsAuthenticated(String name, String password);
    }

    //Callback repuesta al cliente de forma asincrona, todo lo callback es void

    [ServiceContract(CallbackContract = typeof(IAuthenticationServiceCallBack))]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReponseAuthenticated(String username, String email, String password01);
    }
}

