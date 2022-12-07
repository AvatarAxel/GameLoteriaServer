using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(ILoteriaServiceCallBack))]
    public interface ILoteriaService
    {
        [OperationContract(IsOneWay = true)]
        void CreateLoteria(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void DeleteLoteria(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void JoinLoteria(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ExitLoteria(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ReciveWinner(string username,string verificationCode, int totalCoins);
        [OperationContract(IsOneWay = true)]
        void StartGameLoteria(string verificationCode);
    }

    [ServiceContract]
    public interface ILoteriaServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void SendCard(int idCard);
        [OperationContract(IsOneWay = true)]
        void SendWinner(string username);
        [OperationContract(IsOneWay = true)]
        void StopGame(bool status);
    }
}
