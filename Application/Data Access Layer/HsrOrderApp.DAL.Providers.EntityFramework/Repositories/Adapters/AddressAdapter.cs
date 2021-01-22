#region

using System.Data.Objects.DataClasses;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class AddressAdapter
    {
        internal static IQueryable<BL.DomainModel.Address> AdaptAddresses(EntityCollection<Address> addressCollection)
        {
            if (addressCollection.IsLoaded == false)
                return null;

            var addresses = from a in addressCollection.AsEnumerable()
                            select AdaptAddress(a);
            return addresses.AsQueryable();
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