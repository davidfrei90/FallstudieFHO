#region

using System;
using System.ServiceModel;
using HsrOrderApp.SL.DistributionSystemService.OrderSystemService;

#endregion

namespace HsrOrderApp.SL.DistributionSystemService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class DistributionSystemService : IDistributionSystemService
    {
        #region IDistributionSystemService Members

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void ShipOrder(OrderToShipDTO orderToShip)
        {
            try
            {
                OrderShippedDTO order = new OrderShippedDTO();
                order.Id = orderToShip.Id;
                order.ShippedDate = DateTime.Now;
                using (OrderSystemServiceClient orderService = new OrderSystemServiceClient())
                {
                    orderService.OrderShippedNotification(order);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                throw;
            }
            Console.WriteLine(string.Format("Order with ID {0} shipped!", orderToShip.Id));
        }

        #endregion
    }
}