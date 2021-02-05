#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
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

        //************SupplierConditions instead***************
        //public IQueryable<HsrOrderApp.BL.DomainModel.Supplier> GetAll()
        //{
        //    var suppliers = from c in this.db.SupplierSet.Include("Orders").AsEnumerable()
        //                    select SupplierAdapter.AdaptSupplier(c);

        //    return suppliers.AsQueryable();
        //}

        public HsrOrderApp.BL.DomainModel.Supplier GetById(int id)
        {
            try
            {
                var suppliers = from s in this.db.SupplierSet.Include("Addresses").AsEnumerable()
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
                BL.DomainModel.Supplier dbSupplier;

                bool isNew = false;
                if (supplier.SupplierId == default(int) || supplier.SupplierId <= 0)
                {
                    isNew = true;
                    dbSupplier = new Supplier();
                }
                else
                {
                    dbSupplier = new Supplier() {SupplierId = supplier.SupplierId, Version = supplier.Version.ToTimestamp()};
                    dbSupplier.EntityKey = db.CreateEntityKey(setname, dbSupplier);
                    db.AttachTo(setname, dbSupplier);
                }
                //***Add other Properties
                dbSupplier.Name = supplier.Name;
                if (isNew)
                {
                    db.AddToSupplierSet(dbSupplier);
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
            Supplier su = db.SupplierSet.First(s => s.SupplierId == id);
            if (su != null)
            {
                db.DeleteObject(su);
                db.SaveChanges();
            }
        }

        public int SaveAddress(HsrOrderApp.BL.DomainModel.Address address, HsrOrderApp.BL.DomainModel.Supplier forThisSupplier)
        {
            AddressRepository rep = new AddressRepository(db);
            Address dbAddress = rep.SaveAddressInternal(address);
            if (address.IsNew)
            {
                Supplier supplier = db.SupplierSet.First(s => s.SupplierId == forThisSupplier.SupplierId);
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