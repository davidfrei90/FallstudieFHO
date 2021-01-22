#region

using System.Data.Linq;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public class AddressRepository : RepositoryBase
    {
        public AddressRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address)
        {
            try
            {
                Address dbAddress = new Address();
                bool isNew = false;
                if (address.AddressId == default(int) || address.AddressId <= 0)
                {
                    isNew = true;
                }

                dbAddress.AddressId = address.AddressId;
                dbAddress.Version = address.Version.ToTimestamp();
                dbAddress.AddressLine1 = address.AddressLine1;
                dbAddress.AddressLine2 = address.AddressLine2;
                dbAddress.PostalCode = address.PostalCode;
                dbAddress.City = address.City;
                dbAddress.Phone = address.Phone;
                dbAddress.Email = address.Email;

                if (isNew)
                    db.Addresses.InsertOnSubmit(dbAddress);
                else
                    db.Addresses.Attach(dbAddress, true);
                db.SubmitChanges();
                address.AddressId = dbAddress.AddressId;
                return dbAddress.AddressId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteAddress(int id)
        {
            Address cu = db.Addresses.FirstOrDefault(c => c.AddressId == id);
            if (cu != null)
            {
                db.Addresses.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }
    }
}