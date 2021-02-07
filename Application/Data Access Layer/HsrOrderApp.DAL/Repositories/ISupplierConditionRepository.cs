#region

using System.Linq;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.DAL.Data.Repositories
{
    public interface ISupplierConditionRepository
    {
        IQueryable<SupplierCondition> GetAll();
        SupplierCondition GetById(int id);

        int SaveSupplierCondition(SupplierCondition supplierCondition);
        int SaveSupplierCondition(SupplierCondition supplierCondition, bool detached);

        void DeleteSupplierCondition(int id);
    }
}