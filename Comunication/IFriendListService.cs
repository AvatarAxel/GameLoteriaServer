
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IFriendListServiceCallBack))]
    public interface IFriendListService
    {
        [OperationContract]
        int CheckNumberFriends(string email);
        [OperationContract(IsOneWay = true)]
        void SendInvitation(string verificationCode, string usernameSender, string usernameRecipient);
        [OperationContract(IsOneWay = true)]
        void JoinFriend(string verificationCode, string username);
        [OperationContract(IsOneWay = true)]
        void AddFriends(string userEmail, string emailNewFriend, string verificationCode);
    }

    [ServiceContract]
    public interface IFriendListServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ReciveInvitation(bool status, string usernameSender);

        [OperationContract(IsOneWay = true)]
        void AddFriendResponse(bool status);
    }
}