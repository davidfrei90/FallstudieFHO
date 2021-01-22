#region

using System.Data.Objects.DataClasses;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class CustomerAdapter
    {
        internal static BL.DomainModel.Customer AdaptCustomer(EntityReference<Customer> c)
        {
            if (c == null || c.Value == null)
                return null;
            return AdaptCustomer(c.Value, null);
        }

        internal static BL.DomainModel.Customer AdaptCustomer(EntityReference<Customer> c, BL.DomainModel.User user)
        {
            if (c == null || c.Value == null)
                return null;
            return AdaptCustomer(c.Value, user);
        }

        internal static BL.DomainModel.Customer AdaptCustomer(Customer c)
        {
            return AdaptCustomer(c, null);
        }

        internal static BL.DomainModel.Customer AdaptCustomer(Customer c, BL.DomainModel.User user)
        {
            BL.DomainModel.Customer customer = new BL.DomainModel.Customer()
                                                   {
                                                       CustomerId = c.CustomerId,
                                                       Name = c.Name,
                                                       FirstName = c.FirstName,
                                                       Version = c.Version.ToUlong(),
                                                   };
            if (user == null)
                customer.User = SecurityAdapter.AdaptUser(c.UserReference);
            else
                customer.User = user;
            customer.Orders = OrderAdapter.AdaptOrders(c.Orders, customer);
            customer.Addresses = AddressAdapter.AdaptAddresses(c.Addresses);
            return customer;
        }
    }
}