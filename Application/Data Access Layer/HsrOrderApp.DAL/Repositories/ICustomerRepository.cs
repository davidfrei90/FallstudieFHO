#region

using System.Linq;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.DAL.Data.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAll();
        Customer GetById(int id);

        int SaveCustomer(Customer customer);
        int SaveAddress(Address address, Customer forThisCustomer);

        void DeleteCustomer(int id);
        void DeleteAddress(int id);
    }
}