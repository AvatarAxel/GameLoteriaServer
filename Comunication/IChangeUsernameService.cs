using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
