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
    [ServiceContract]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract]
        void ReponseAuthenticated(bool result);
    }
}

