#region

using System;
using System.Data.Linq;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.LinqToSql.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public CustomerRepository(string connectionString) : base(connectionString)
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Customer> GetAll()
        {
            var customers = from c in this.db.Customers
                            select CustomerAdapter.AdaptCustomer(c);

            return customers;
        }

        public HsrOrderApp.BL.DomainModel.Customer GetById(int id)
        {
            try
            {
                var customers = from c in this.db.Customers
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
                Customer dbCustomer = new Customer();
                bool isNew = false;
                if (customer.CustomerId == default(int) || customer.CustomerId <= 0)
                    isNew = true;

                dbCustomer.CustomerId = customer.CustomerId;
                dbCustomer.Version = customer.Version.ToTimestamp();
                dbCustomer.Name = customer.Name;
                dbCustomer.FirstName = customer.FirstName;

                if (isNew)
                    db.Customers.InsertOnSubmit(dbCustomer);
                else
                    db.Customers.Attach(dbCustomer, true);
                db.SubmitChanges();
                customer.CustomerId = dbCustomer.CustomerId;
                return dbCustomer.CustomerId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteCustomer(int id)
        {
            Customer cu = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (cu != null)
            {
                db.Customers.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Customer forThisCustomer)
        {
            AddressRepository rep = new AddressRepository(db);
            int addressid = rep.SaveAddress(address);
            if (address.IsNew)
            {
                CustomerAddress ca = new CustomerAddress();
                ca.AddressId = addressid;
                ca.CustomerId = forThisCustomer.CustomerId;
                db.CustomerAddresses.InsertOnSubmit(ca);
                db.SubmitChanges();
            }
            return addressid;
        }

        public void DeleteAddress(int id)
        {
            AddressRepository rep = new AddressRepository(db);
            rep.DeleteAddress(id);
        }
    }
}