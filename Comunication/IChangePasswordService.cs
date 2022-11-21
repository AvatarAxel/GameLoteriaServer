using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    internal interface IChangePasswordService
    {
        [OperationContract]
        bool ChangePassword(string email, string password);
    }
}
