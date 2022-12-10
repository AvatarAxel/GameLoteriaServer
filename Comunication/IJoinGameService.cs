using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    public interface IJoinGameService
    {
        [OperationContract]
        bool ResponseCodeExist(string verificationCode);
        [OperationContract]
        bool ResponseCompleteLobby(string verificationCode);
        [OperationContract]
        bool ValidateCoinsUnregistered(int coins, string verificationCode);
        [OperationContract]
        bool ValidateCoinsRegistered(string username, string verificationCode);
    }
}
