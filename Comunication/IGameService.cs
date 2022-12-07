﻿using Logic;
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
        void UpdateTotalPlayers(string verificationCode);
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
    }
}
