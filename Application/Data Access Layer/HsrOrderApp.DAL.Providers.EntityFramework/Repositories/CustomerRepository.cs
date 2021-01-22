#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public CustomerRepository() : base()
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Customer> GetAll()
        {
            var customers = from c in this.db.CustomerSet.Include("Orders").AsEnumerable()
                            select CustomerAdapter.AdaptCustomer(c);

            return customers.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.Customer GetById(int id)
        {
            try
            {
                var customers = from c in this.db.CustomerSet.Include("Addresses").AsEnumerable()
                                where c.CustomerId == id
                                select CustomerAdapter.AdaptCustomer(c);

                return customers.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingCustomer();
            }
        }

        public int SaveCustomer(HsrOrderApp.BL.DomainModel.Customer customer)
        {
            try
            {
                string setname = "CustomerSet";
                Customer dbCustomer;

                bool isNew = false;
                if (customer.CustomerId == default(int) || customer.CustomerId <= 0)
                {
                    isNew = true;
                    dbCustomer = new Customer();
                }
                else
                {
                    dbCustomer = new Customer() {CustomerId = customer.CustomerId, Version = customer.Version.ToTimestamp()};
                    dbCustomer.EntityKey = db.CreateEntityKey(setname, dbCustomer);
                    db.AttachTo(setname, dbCustomer);
                }

                dbCustomer.Name = customer.Name;
                dbCustomer.FirstName = customer.FirstName;
                if (isNew)
                {
                    db.AddToCustomerSet(dbCustomer);
                }
                db.SaveChanges();

                customer.CustomerId = dbCustomer.CustomerId;
                return dbCustomer.CustomerId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteCustomer(int id)
        {
            Customer cu = db.CustomerSet.First(c => c.CustomerId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Customer forThisCustomer)
        {
            AddressRepository rep = new AddressRepository(db);
            Address dbAddress = rep.SaveAddressInternal(address);
            if (address.IsNew)
            {
                Customer customer = db.CustomerSet.First(c => c.CustomerId == forThisCustomer.CustomerId);
                customer.Addresses.Add(dbAddress);
                db.SaveChanges();
            }
            return dbAddress.AddressId;
        }

        public void DeleteAddress(int id)
        {
            AddressRepository rep = new AddressRepository(db);
            rep.DeleteAddress(id);
        }
    }
}