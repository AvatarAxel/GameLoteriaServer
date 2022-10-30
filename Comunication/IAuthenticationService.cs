﻿using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text; 
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IAuthenticationServiceCallBack))]
    public interface IAuthenticationService
    {
        [OperationContract(IsOneWay = true)]
        void AuthenticationLogin(String name, String password);

        [OperationContract(IsOneWay = true)]
        void RegistrerUserBD(PlayerDTO player);
    }

    [ServiceContract]
    public interface IAuthenticationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseAuthenticated(bool status);
    }

    [ServiceContract(CallbackContract = typeof(IChatServiceCallBack))]
    public interface IChat
    {
        [OperationContract(IsOneWay = true)]
        void JoinChat(string username);
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string userChat);
        [OperationContract(IsOneWay = true)]
        void ExitChat(string userName);
    }

    [ServiceContract]
    public interface IChatServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveMessage(string player, string message);
    }
}

