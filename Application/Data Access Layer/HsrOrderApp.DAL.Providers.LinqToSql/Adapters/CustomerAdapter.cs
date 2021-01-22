#region

using System.Data.Linq;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal static class CustomerAdapter
    {
        internal static BL.DomainModel.Customer AdaptCustomer(Customer c)
        {
            return AdaptCustomer(c, null);
        }

        internal static BL.DomainModel.Customer AdaptCustomer(Customer c, BL.DomainModel.User user)
        {
            if (c == null) return null;
            BL.DomainModel.Customer customer = new BL.DomainModel.Customer()
                                                   {
                                                       CustomerId = c.CustomerId,
                                                       Name = c.Name,
                                                       FirstName = c.FirstName,
                                                       Version = c.Version.ToUlong(),
                                                   };
            if (user == null && c.User != null)
                customer.User = SecurityAdapter.AdaptUser(c.User.FirstOrDefault(), customer);
            else if (user != null)
                customer.User = user;
            customer.Orders = OrderAdapter.AdaptOrders(c.Orders, customer);
            customer.Addresses = AddressAdapter.AdaptAddresses(c.Addresses);
            return customer;
        }
    }
}