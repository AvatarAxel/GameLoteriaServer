using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    public interface IJoinGameService
    {
        [OperationContract]
        bool ResponseCodeExist(string verificationCode);
        [OperationContract]
        bool ResponseCompleteLobby(string verificationCode);
        [OperationContract]
        bool ResponseUsernameExist(string verificationCode, string username);
    }
}
