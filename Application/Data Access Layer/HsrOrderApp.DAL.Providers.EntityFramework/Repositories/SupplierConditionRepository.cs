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
            var SupplierConditions = from c in this.db.SupplierConditions.AsEnumerable()
                           select SupplierConditionAdapter.AdaptSupplierCondition(c);

            return SupplierConditions.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.SupplierCondition GetById(int id)
        {
            try
            {
                var supplierConditions = from c in this.db.SupplierConditions.AsEnumerable()
                               where c.SupplierConditionId == id
                               select SupplierConditionAdapter.AdaptSupplierCondition(c);

                return supplierConditions.First();
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
                    dbSupplierCondition = new SupplierCondition() { SupplierConditionId = supplierCondition.SupplierConditionId, Version = supplierCondition.Version.ToTimestamp() };
                    dbSupplierCondition.EntityKey = db.CreateEntityKey(setname, dbSupplierCondition);
                    db.AttachTo(setname, dbSupplierCondition);
                }
                //dbSupplierCondition.ProductId = SupplierCondition.ProductId;
                //dbSupplierCondition.SupplierId = SupplierCondition.SupplierId;
                //dbSupplierCondition.StandardPrice = SupplierCondition.StandardPrice;
                //dbSupplierCondition.LastReceiptCost = SupplierCondition.LastReceiptCost;
                //dbSupplierCondition.LastReceiptDate = SupplierCondition.LastReceiptDate;
                //dbSupplierCondition.MinOrderQty = SupplierCondition.MinOrderQty;
                //dbSupplierCondition.MaxOrderQty = SupplierCondition.MaxOrderQty;



                if (isNew)
                {
                    db.AddToSupplierConditions(dbSupplierCondition);
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
            SupplierCondition cu = db.SupplierConditions.First(c => c.SupplierConditionId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }
    }
}