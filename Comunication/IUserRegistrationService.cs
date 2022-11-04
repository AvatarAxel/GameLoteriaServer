using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IUserRegistrationServiceCallBack))]
    public interface IUserRegistrationService
    {
        [OperationContract(IsOneWay = true)]
        void RegistrerUserBD(PlayerDTO player);
    }
    [ServiceContract]
    public interface IUserRegistrationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseRegister(bool status);
    }
}
