#region

using System.Linq;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.DAL.Data.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        Product GetById(int id);

        int SaveProduct(Product product);

        void DeleteProduct(int id);
    }
}