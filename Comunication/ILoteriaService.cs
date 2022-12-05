using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(ILoteriaServiceCallBack))]
    public interface ILoteriaService
    {
        [OperationContract]
        void CreateLoteria(string verificationCode);
        [OperationContract]
        void DeleteLoteria(string verificationCode);
        [OperationContract]
        void JoinLoteria(string verificationCode);
        [OperationContract]
        void ExitLoteria(string verificationCode);
        [OperationContract]
        void ReciveWinner(string verificationCode);

    }
    [ServiceContract]
    public interface ILoteriaServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void SendCard(int idCard);
    }
}
