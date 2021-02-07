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
    public class SupplierRepository : RepositoryBase, ISupplierRepository
    {
        public SupplierRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public SupplierRepository(string connectionString) : base(connectionString)
        {
        }

        public SupplierRepository() : base()
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Supplier> GetAll()
        {
            var suppliers = from s in this.db.Suppliers.Include("SupplierConditions").AsEnumerable()
                            select SupplierAdapter.AdaptSupplier(s);

            return suppliers.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.Supplier GetById(int id)
        {
            try
            {
                var suppliers = from s in this.db.Suppliers.Include("Addresses").AsEnumerable()
                                where s.SupplierId == id
                                select SupplierAdapter.AdaptSupplier(s);

                return suppliers.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingSupplier();
            }
        }

        public int SaveSupplier(HsrOrderApp.BL.DomainModel.Supplier supplier)
        {
            try
            {
                string setname = "SupplierSet";
                Supplier dbSupplier;

                bool isNew = false;
                if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0)
                {
                    isNew = true;
                    dbSupplier = new Supplier();
                }
                else
                {
                    dbSupplier = new Supplier() { SupplierId = supplier.SupplierId, Version = supplier.Version.ToTimestamp()};
                    dbSupplier.EntityKey = db.CreateEntityKey(setname, dbSupplier);
                    db.AttachTo(setname, dbSupplier);
                }

                dbSupplier.AccountNumber = supplier.AccountNumber;
                dbSupplier.Name = supplier.Name;
                dbSupplier.CreditRating = supplier.CreditRating;
                dbSupplier.PreferredSupplierFlag = supplier.PreferredSupplierFlag;
                dbSupplier.ActiveFlag = supplier.ActiveFlag;
                dbSupplier.PurchasingWebServiceURL = supplier.PurchasingWebServiceURL;

                if (isNew)
                {
                    db.AddToSuppliers(dbSupplier);
                }
                db.SaveChanges();

                supplier.SupplierId = dbSupplier.SupplierId;
                return dbSupplier.SupplierId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteSupplier(int id)
        {
            Supplier su = db.Suppliers.First(s => s.SupplierId == id);
            if (su != null)
            {
                db.DeleteObject(su);
                db.SaveChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Supplier forThisCustomer)
        {
            AddressRepository rep = new AddressRepository(db);
            Address dbAddress = rep.SaveAddressInternal(address);
            if (address.IsNew)
            {
                Supplier supplier = db.Suppliers.First(s => s.SupplierId == forThisCustomer.SupplierId);
                supplier.Addresses.Add(dbAddress);
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