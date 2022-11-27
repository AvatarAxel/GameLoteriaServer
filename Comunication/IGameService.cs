using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IGameServiceCallBack))]
    public interface IGameService
    {
        [OperationContract(IsOneWay = true)]
        void JoinGame(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void SendWinner(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ExitGame(string userName, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void CreateGame(string verificationCode, int limitPlayers);
        [OperationContract(IsOneWay = true)]
        void EliminateGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void SendNextHostGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void GoToGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void StartGame(string verificationCode, int speed);
    }

    [ServiceContract]
    public interface IGameServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveWinner(string username);
        [OperationContract(IsOneWay = true)]
        void ResponseTotalPlayers(int totalPlayers);
        [OperationContract(IsOneWay = true)]
        void SendNextHostGameResponse(bool status);
        [OperationContract(IsOneWay = true)]
        void SendCard(int idCard);
        [OperationContract(IsOneWay = true)]
        void GoToPlay(bool status);
    }
}
