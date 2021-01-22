#region

using System.Linq;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.DAL.Data.Repositories
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        IQueryable<OrderDetail> GetAllDetails();
        Order GetById(int id);

        int SaveOrder(Order order);
        int SaveOrder(Order order, bool detached);
        int SaveOrderDetail(OrderDetail detail, Order forThisOrder);

        void DeleteOrder(int id);
        void DeleteOrderDetail(int id);
    }
}