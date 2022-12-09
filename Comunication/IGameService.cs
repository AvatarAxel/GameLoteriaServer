using Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        void CreateGame(GameRoundDTO game);
        [OperationContract(IsOneWay = true)]
        void EliminateGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void SendNextHostGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void GoToGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void StartGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void BanPlayer(string verificationCode, string username);

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
        [OperationContract(IsOneWay = true)]
        void GetListPlayer(List<string> PlayerLobby);
        [OperationContract(IsOneWay = true)]
        void BanPlayerResponse(bool status);
    }
}
