using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IEmailServiceCallBack))]
    public interface IEmailService
    {
        [OperationContract(IsOneWay = true)]
        void ValidationEmail(String email);
    }
    public interface IEmailServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void ResponseEmail(string verificationCode);
    }
}
