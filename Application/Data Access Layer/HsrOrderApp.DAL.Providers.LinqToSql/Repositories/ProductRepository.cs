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
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public ProductRepository(string connectionString) : base(connectionString)
        {
        }

        public IQueryable<HsrOrderApp.BL.DomainModel.Product> GetAll()
        {
            var products = from p in this.db.Products
                           select ProductAdapter.AdaptProduct(p);

            return products;
        }

        public HsrOrderApp.BL.DomainModel.Product GetById(int id)
        {
            try
            {
                var products = from p in this.db.Products
                               where p.ProductId == id
                               select ProductAdapter.AdaptProduct(p);

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
                Product dbProduct = new Product();
                bool isNew = false;
                if (product.ProductId == default(int) || product.ProductId <= 0)
                    isNew = true;

                dbProduct.ProductId = product.ProductId;
                dbProduct.Version = product.Version.ToTimestamp();
                dbProduct.Name = product.Name;
                dbProduct.Category = product.Category;
                dbProduct.QuantityPerUnit = product.QuantityPerUnit;
                dbProduct.ListUnitPrice = product.ListUnitPrice;
                dbProduct.UnitsOnStock = product.UnitsOnStock;
                dbProduct.ProductNumber = product.ProductNumber;

                if (isNew)
                    db.Products.InsertOnSubmit(dbProduct);
                else
                    db.Products.Attach(dbProduct, true);
                db.SubmitChanges();
                product.ProductId = dbProduct.ProductId;
                return dbProduct.ProductId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void DeleteProduct(int id)
        {
            Product cu = db.Products.FirstOrDefault(c => c.ProductId == id);
            if (cu != null)
            {
                db.Products.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }
    }
}