using System;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    public interface IEmailService
    {
        [OperationContract]
        bool ValidationEmail(String email, string codeVerification);
    }
}
