using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IJoinGameServiceCallBack))]
    public interface IJoinGameService
    {
        [OperationContract(IsOneWay = true)]
        void JoinGame(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void SendWinner(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ExitGame(string userName, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void CreateGame(string verificationCode);
    }

    [ServiceContract]
    public interface IJoinGameServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveWinner(string username);
        [OperationContract(IsOneWay = true)]
        void CodeExist(bool status);
    }
}
