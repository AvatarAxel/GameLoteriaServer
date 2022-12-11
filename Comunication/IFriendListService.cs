using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    public interface IFriendListService
    {
        [OperationContract]
        int CheckNumberFriends(string email);
    }
}
