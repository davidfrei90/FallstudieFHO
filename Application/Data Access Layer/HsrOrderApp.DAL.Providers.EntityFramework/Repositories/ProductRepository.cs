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
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public ProductRepository() : base()
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Product> GetAll()
        {
            var Products = from c in this.db.ProductSet.AsEnumerable()
                           select ProductAdapter.AdaptProduct(c);

            return Products.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.Product GetById(int id)
        {
            try
            {
                var products = from c in this.db.ProductSet.AsEnumerable()
                               where c.ProductId == id
                               select ProductAdapter.AdaptProduct(c);

                return products.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingProduct();
            }
        }

        public int SaveProduct(HsrOrderApp.BL.DomainModel.Product product)
        {
            try
            {
                string setname = "ProductSet";
                Product dbProduct;

                bool isNew = false;
                if (product.ProductId == default(int) || product.ProductId <= 0)
                {
                    isNew = true;
                    dbProduct = new Product();
                }
                else
                {
                    dbProduct = new Product() {ProductId = product.ProductId, Version = product.Version.ToTimestamp()};
                    dbProduct.EntityKey = db.CreateEntityKey(setname, dbProduct);
                    db.AttachTo(setname, dbProduct);
                }
                dbProduct.Name = product.Name;
                dbProduct.Category = product.Category;
                dbProduct.QuantityPerUnit = product.QuantityPerUnit;
                dbProduct.ListUnitPrice = product.ListUnitPrice;
                dbProduct.UnitsOnStock = product.UnitsOnStock;
                dbProduct.ProductNumber = product.ProductNumber;

                if (isNew)
                {
                    db.AddToProductSet(dbProduct);
                }
                db.SaveChanges();
                product.ProductId = dbProduct.ProductId;
                return dbProduct.ProductId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteProduct(int id)
        {
            Product cu = db.ProductSet.First(c => c.ProductId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }
    }
}