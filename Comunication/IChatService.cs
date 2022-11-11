using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IChatServiceCallBack))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = true)]
        void JoinChat(string username, string codeVerification);
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, string userChat, string codeVerification);
        [OperationContract(IsOneWay = true)]
        void ExitChat(string userName, string codeVerification);
        [OperationContract(IsOneWay = true)]
        void CreateChat(string verificationCode);
        [OperationContract(IsOneWay = true)]
        void DeleteChat(string verificationCode);
    }

    [ServiceContract]
    public interface IChatServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveMessage(string player, string message);
    }
}
