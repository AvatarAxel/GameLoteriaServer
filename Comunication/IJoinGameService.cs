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
    }
}
