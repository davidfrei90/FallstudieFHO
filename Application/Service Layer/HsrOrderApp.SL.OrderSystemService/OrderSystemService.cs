#region

using System;
using System.ServiceModel;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;

#endregion

namespace HsrOrderApp.SL.OrderSystemService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class OrderSystemService : IOrderSystemService
    {
        #region IOrderSystemService Members

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void OrderShippedNotification(OrderShippedDTO order)
        {
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();
            bc.SetOrderShipped(order.Id, order.ShippedDate);

            Console.WriteLine(string.Format("Status of Order with ID {0} updated!", order.Id));
        }

        #endregion
    }
}