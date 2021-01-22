#region

using System.Data.Linq;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal static class AddressAdapter
    {
        
        internal static IQueryable<BL.DomainModel.Address> AdaptAddresses(EntitySet<CustomerAddress> addressCollection)
        {
            var addresses = from a in addressCollection.AsQueryable()
                            select AdaptAddress(a.Address);
            return addresses;
        }

        internal static BL.DomainModel.Address AdaptAddress(Address a)
        {
            return new BL.DomainModel.Address()
                       {
                           AddressId = a.AddressId,
                           AddressLine1 = a.AddressLine1,
                           AddressLine2 = a.AddressLine2,
                           PostalCode = a.PostalCode,
                           City = a.City,
                           Phone = a.Phone,
                           Email = a.Email,
                           Version = a.Version.ToUlong(),
                       };
        }
    }
}