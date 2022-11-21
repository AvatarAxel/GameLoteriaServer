using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    public interface IUserRegistrationService
    {
        [OperationContract]
        bool RegistrerUserDataBase(PlayerDTO player);

        [OperationContract]
        bool ValidationEmailDataBase(string email);

        [OperationContract]
        bool ValidationUsernameDataBase(string username);
    }

}
