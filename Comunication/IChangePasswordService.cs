using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IChangePasswordServiceCallBack))]
    internal interface IChangePasswordService
    {
        [OperationContract(IsOneWay = true)]
        void ChangePassword(string email, string password);
    }
    [ServiceContract]
    public interface IChangePasswordServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseChangePassword(bool status);
    }
}
