#region

using System.Data;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    internal class AddressRepository : RepositoryBase
    {
        public AddressRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }


        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address)
        {
            Address dbAddress = SaveAddressInternal(address);
            address.AddressId = dbAddress.AddressId;
            return address.AddressId;
        }

        internal Address SaveAddressInternal(BL.DomainModel.Address address)
        {
            try
            {
                string setname = "AddressSet";
                Address dbAddress;
                bool isNew = false;
                if (address.AddressId == default(int) || address.AddressId <= 0)
                {
                    isNew = true;
                    dbAddress = new Address();
                }
                else
                {
                    dbAddress = new Address() {AddressId = address.AddressId, Version = address.Version.ToTimestamp()};
                    dbAddress.EntityKey = db.CreateEntityKey(setname, dbAddress);
                    db.AttachTo(setname, dbAddress);
                }
                dbAddress.AddressLine1 = address.AddressLine1;
                dbAddress.AddressLine2 = address.AddressLine2;
                dbAddress.PostalCode = address.PostalCode;
                dbAddress.City = address.City;
                dbAddress.Phone = address.Phone;
                dbAddress.Email = address.Email;


                if (isNew)
                {
                    db.AddToAddressSet(dbAddress);
                }
                db.SaveChanges();

                return dbAddress;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return null;
            }
        }

        public void DeleteAddress(int id)
        {
            Address cu = db.AddressSet.FirstOrDefault(c => c.AddressId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }
    }
}