
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IFriendListServiceCallBack))]
    public interface IFriendListService
    {
        [OperationContract]
        int CheckNumberFriends(string email);
        [OperationContract]
        void SendInvitation(string verificationCode, string usernameSender, string usernameRecipient);
        [OperationContract]
        void JoinFriend(string verificationCode, string username);
    }

    [ServiceContract]
    public interface IFriendListServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveInvitation(bool status, string usernameSender);
    }
}