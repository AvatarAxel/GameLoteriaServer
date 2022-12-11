using Logic;
using System.Collections.Generic;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IGameServiceCallBack))]
    public interface IGameService
    {
        [OperationContract(IsOneWay = true)]
        void JoinGame(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void ExitGame(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void CreateGame(GameRoundDTO game);
        [OperationContract(IsOneWay = true)]
        void EliminateGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void SendNextHostGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void GoToGame(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void UpdateTotalPlayers(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void UpdateBetCoins(string username, string verificationCode);
        [OperationContract(IsOneWay = true)]
        void BanPlayer(string verificationCode, string username);

    }

    [ServiceContract]
    public interface IGameServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseTotalPlayers(int totalPlayers);
        [OperationContract(IsOneWay = true)]
        void SendNextHostGameResponse(bool status);
        [OperationContract(IsOneWay = true)]
        void GoToPlay(bool status);
        [OperationContract(IsOneWay = true)]
        void UpdateBetCoinsResponse(int coins, int bet);
        [OperationContract(IsOneWay = true)]
        void GetListPlayer(List<string> PlayerLobby);
        [OperationContract(IsOneWay = true)]
        void BanPlayerResponse(bool status);
    }
}
