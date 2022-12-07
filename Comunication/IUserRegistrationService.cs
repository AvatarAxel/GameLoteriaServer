using Logic;
using System.ServiceModel;

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
