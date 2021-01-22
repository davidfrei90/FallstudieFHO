#region

using System.ServiceModel;

#endregion

namespace HsrOrderApp.SL.DistributionSystemService
{
    [ServiceContract(Namespace = "order.hsr.ch")]
    public interface IDistributionSystemService
    {
        [OperationContract(IsOneWay = true)]
        void ShipOrder(OrderToShipDTO order);
    }
}