using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    internal interface IChangePasswordService
    {
        [OperationContract]
        bool ChangePassword(string email, string password);
    }
}
