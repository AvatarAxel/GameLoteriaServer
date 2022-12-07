using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    internal interface IChangeUsernameService
    {
        [OperationContract]
        bool ChangeUsername(string email, string username);        
        [OperationContract]
        bool ValidateAvailabilityUsername(string username);
    }
}
