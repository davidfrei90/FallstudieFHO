#region

using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;

#endregion

namespace HsrOrderApp.SL.OrderSystemService
{
    [ServiceContract(Namespace = "order.hsr.ch")]
//    [ValidationBehavior]
    [ExceptionShielding]
    public interface IOrderSystemService
    {
        [OperationContract(IsOneWay = true)]
        void OrderShippedNotification(OrderShippedDTO order);
    }
}