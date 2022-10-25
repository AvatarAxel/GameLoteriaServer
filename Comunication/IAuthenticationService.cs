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

        [OperationContract(IsOneWay = true)]
        void ReponseAuthenticated(string username, string email, string password01);

    }

    //Callback repuesta al cliente de forma asincrona, todo lo callback es void

    [ServiceContract]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void NombreEjemplo(bool status);
    }
}

