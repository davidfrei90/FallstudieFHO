#region

using System;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public class SupplierConditionRepository : RepositoryBase, ISupplierConditionRepository
    {
        public SupplierConditionRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public SupplierConditionRepository(string connectionString) : base(connectionString)
        {
        }

        public SupplierConditionRepository() : base()
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.SupplierCondition> GetAll()
        {
            var supplierconditions = from s in this.db.SupplierConditions.Include("Supplier").AsEnumerable()
                         select SupplierConditionAdapter.AdaptSupplierCondition(s);

            return supplierconditions.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.SupplierCondition GetById(int id)
        {
            try
            {
                var supplierconditions = from s in this.db.SupplierConditions.Include("Supplier").AsEnumerable()
                             where s.SupplierConditionId == id
                             select SupplierConditionAdapter.AdaptSupplierCondition(s);

                return supplierconditions.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingSupplierCondition();
            }
        }


        public int SaveSupplierCondition(HsrOrderApp.BL.DomainModel.SupplierCondition supplierCondition)
        {
            return SaveSupplierCondition(supplierCondition, true);
        }

        public int SaveSupplierCondition(HsrOrderApp.BL.DomainModel.SupplierCondition supplierCondition, bool detached)
        {
            try
            {
                SupplierRepository sr = new SupplierRepository(this.db);
                ProductRepository pr = new ProductRepository(this.db);
                string setname = "SupplierConditionSet";

                bool isNew = false;
                SupplierCondition dbSupplierCondition;
                if (detached == false)
                {
                    dbSupplierCondition = db.SupplierConditions.FirstOrDefault(o =>
                        o.SupplierConditionId == supplierCondition.SupplierConditionId);
                }
                else if (supplierCondition.SupplierConditionId == default(int) ||
                         supplierCondition.SupplierConditionId <= 0)
                {
                    isNew = true;
                    dbSupplierCondition = new SupplierCondition();
                }
                else
                {

                    dbSupplierCondition = new SupplierCondition()
                    {
                        SupplierConditionId = supplierCondition.SupplierConditionId,
                        Version = supplierCondition.Version.ToTimestamp(),
                    };
                    dbSupplierCondition.EntityKey = db.CreateEntityKey(setname, dbSupplierCondition);
                    db.AttachTo(setname, dbSupplierCondition);
                }

                dbSupplierCondition.StandardPrice = supplierCondition.StandardPrice;
                dbSupplierCondition.LastReceiptCost = supplierCondition.LastReceiptCost;
                dbSupplierCondition.MinOrderQty = supplierCondition.MinOrderQty;
                dbSupplierCondition.MaxOrderQty = supplierCondition.MaxOrderQty;

                if (isNew)
                {
                    dbSupplierCondition.SuppliersReference.EntityKey = new EntityKey("HsrOrderAppEntities.SupplierSet",
                        "SupplierId", supplierCondition.Supplier.SupplierId);
                    db.AddToSupplierConditions(dbSupplierCondition);
                }
                else
                {
                    dbSupplierCondition.SuppliersReference.Attach(db.SupplierSet.First(s =>
                        s.SupplierId == supplierCondition.Supplier.SupplierId));
                }

                db.SaveChanges();
                supplierCondition.SupplierConditionId = dbSupplierCondition.SupplierConditionId;
                return dbSupplierCondition.SupplierConditionId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

       

        public void DeleteSupplierCondition(int id)
        {
            // TODO: Delete OrderDetails first
            SupplierCondition su = db.SupplierConditions.First(s => s.SupplierConditionId == id);
            if (su != null)
            {
                db.DeleteObject(su);
                db.SaveChanges();
            }
        }

    }
}