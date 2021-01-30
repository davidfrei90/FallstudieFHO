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
            var SupplierCondition = from c in this.db.SupplierConditionSet.AsEnumerable()
                           select SupplierConditionAdapter.AdaptSupplierCondition(c);

            return SupplierCondition.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.SupplierCondition GetById(int id)
        {
            try
            {
                var supplierCondition = from c in this.db.SupplierConditionSet.AsEnumerable()
                               where c.SupplierConditionId == id
                               select SupplierConditionAdapter.AdaptSupplierCondition(c);

                return supplierCondition.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingSupplierCondition();
            }
        }

        public int SaveSupplierCondition(HsrOrderApp.BL.DomainModel.SupplierCondition supplierCondition)
        {
            try
            {
                string setname = "SupplierConditionSet";
                SupplierCondition dbSupplierCondition;

                bool isNew = false;
                if (supplierCondition.SupplierConditionId == default(int) || supplierCondition.SupplierConditionId <= 0)
                {
                    isNew = true;
                    dbSupplierCondition = new SupplierCondition();
                }
                else
                {
                    dbSupplierCondition = new SupplierCondition() { SupplierConditionId = supplierCondition.SupplierConditionId, Version = supplierCondition.Version.ToTimestamp()};
                    dbSupplierCondition.EntityKey = db.CreateEntityKey(setname, dbSupplierConditiont);
                    db.AttachTo(setname, dbSupplierCondition);
                }
                dbSupplierCondition.ProductId = supplierCondition.ProductId;
                dbSupplierCondition.SupplierId = supplierCondition.SupplierId;
                dbSupplierCondition.StandardPrice = supplierCondition.StandardPrice;
                dbSupplierCondition.LastReceiptCost = supplierCondition.LastReceiptCost;
                dbSupplierCondition.LastReceiptDate = supplierCondition.LastReceiptDate;
                dbSupplierCondition.MinOrderQty = supplierCondition.MinOrderQty;
                dbSupplierCondition.MaxOrderQty = supplierCondition.MaxOrderQty;
                if (isNew)
                {
                    db.AddToSupplierConditionSet(dbSupplierCondition);
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
            SupplierCondition cu = db.SupplierConditionSet.First(c => c.SupplierConditionId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }
    }
}