#region

using System;
using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.UI.PresentationLogic
{
    public interface IServiceFacade : IDisposable
    {
        OrderDTO GetOrderById(int id);
        IList<OrderListDTO> GetOrdersByCustomer(int customerId);
        IList<OrderListDTO> GetAllOrders();
        void StoreOrder(OrderDTO order);
        void DeleteOrder(int orderId);

        CustomerDTO GetCustomerById(int id);
        IList<CustomerListDTO> GetCustomersByName(string name);
        IList<CustomerListDTO> GetCustomersByCity(string city);
        IList<CustomerListDTO> GetAllCustomers();
        void StoreCustomer(CustomerDTO customer);
        void DeleteCustomer(int customerId);
               
        ProductDTO GetProductById(int id);
        IList<ProductDTO> GetProductsByName(string name);
        IList<ProductDTO> GetProductsByCategory(string category);
        IList<ProductDTO> GetAllProducts();
        void StoreProduct(ProductDTO product);
        void DeleteProduct(int productId);
        void GetEstimatedDeliveryTime(int productId, out int unitsAvailable, out int estimatedDeliveryTime);

        CurrentUserDTO GetCurrentUser();
        UserDTO GetUserById(int id);
        IList<UserListDTO> GetUsersByName(string name);
        IList<UserListDTO> GetUsersByRole(string role);
        IList<UserListDTO> GetAllUsers();
        void StoreUser(UserDTO user);
        void DeleteUser(int userId);

        RoleDTO GetRoleById(int id);
        IList<RoleDTO> GetRolesByName(string name);
        IList<RoleDTO> GetAllRoles();
    }
}