#region

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.UI.PresentationLogic
{
    public class ServiceFacade : IServiceFacade
    {
        #region static private fields

        private static ServiceFacade _instance;
        private static IAdminService _service;

        #endregion

        #region singleton

        private ServiceFacade()
        {
            if (_service == null)
                _service = ServiceFactory.GetCreatorInstance().CreateBusinessLayerInstance();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IServiceFacade GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ServiceFacade();
            }
            return _instance;
        }

        /// <summary>
        /// ctor for testing
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IServiceFacade GetInstance(IAdminService service)
        {
            _service = service;
            if (_instance == null)
            {
                _instance = new ServiceFacade();
            }
            return _instance;
        }

        #endregion

        #region Properties

        public IAdminService Service
        {
            get
            {
                if (_service == null)
                    _service = ServiceFactory.GetCreatorInstance().CreateBusinessLayerInstance();
                return _service;
            }
        }


        #endregion

        #region Order

        public OrderDTO GetOrderById(int id)
        {
            try
            {
                GetOrderRequest request = new GetOrderRequest();
                request.Id = id;

                GetOrderResponse response = Service.GetOrderById(request);
                return response.Order;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new OrderDTO();
            }
        }

        public IList<OrderListDTO> GetOrdersByCustomer(int customerId)
        {
            return getOrders(OrderSearchType.ByCustomer, customerId);
        }

        public IList<OrderListDTO> GetAllOrders()
        {
            return getOrders(OrderSearchType.None, default(int));
        }

        public void StoreOrder(OrderDTO order)
        {
            try
            {
                StoreOrderRequest request = new StoreOrderRequest();
                request.Order = order;
                StoreOrderResponse response = Service.StoreOrder(request);
                order.Id = response.Id;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public void DeleteOrder(int orderId)
        {
            try
            {
                DeleteOrderRequest request = new DeleteOrderRequest();
                request.Id = orderId;
                DeleteOrderResponse response = Service.DeleteOrder(request);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        private IList<OrderListDTO> getOrders(OrderSearchType searchType, int customerid)
        {
            try
            {
                GetOrdersRequest request = new GetOrdersRequest();
                request.SearchType = searchType;
                request.CustomerId = customerid;
                GetOrdersResponse response = Service.GetOrdersByCriteria(request);
                return response.Orders;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new List<OrderListDTO>();
            }
        }

        #endregion

        #region Customer

        public CustomerDTO GetCustomerById(int id)
        {
            try
            {
                GetCustomerRequest request = new GetCustomerRequest();
                request.CustomerId = id;
                GetCustomerResponse response = Service.GetCustomerById(request);
                return response.Customer;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new CustomerDTO();
            }
        }

        public IList<CustomerListDTO> GetCustomersByName(string name)
        {
            return getCustomers(CustomerSearchType.ByName, name, default(string));
        }

        public IList<CustomerListDTO> GetCustomersByCity(string city)
        {
            return getCustomers(CustomerSearchType.ByName, default(string), city);
        }

        public IList<CustomerListDTO> GetAllCustomers()
        {
            return getCustomers(CustomerSearchType.None, default(string), default(string));
        }

        public void StoreCustomer(CustomerDTO customer)
        {
            try
            {
                StoreCustomerRequest request = new StoreCustomerRequest();
                request.Customer = customer;
                StoreCustomerResponse response = Service.StoreCustomer(request);
                customer.Id = response.CustomerId;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            try
            {
                DeleteCustomerRequest request = new DeleteCustomerRequest();
                request.CustomerId = customerId;
                DeleteCustomerResponse response = Service.DeleteCustomer(request);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        private IList<CustomerListDTO> getCustomers(CustomerSearchType searchType, string name, string city)
        {
            try
            {
                GetCustomersRequest request = new GetCustomersRequest();
                request.SearchType = searchType;
                request.CustomerName = name;
                request.City = city;
                GetCustomersResponse response = Service.GetCustomersByCriteria(request);
                return response.Customers;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new List<CustomerListDTO>();
            }
        }

        #endregion

        #region Product

        public ProductDTO GetProductById(int id)
        {
            try
            {
                GetProductRequest request = new GetProductRequest();
                request.Id = id;
                GetProductResponse response = Service.GetProductById(request);
                return response.Product;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new ProductDTO();
            }
        }

        public IList<ProductDTO> GetProductsByName(string name)
        {
            return getProducts(ProductSearchType.ByName, name, default(string));
        }

        public IList<ProductDTO> GetProductsByCategory(string category)
        {
            return getProducts(ProductSearchType.ByCategory, default(string), category);
        }

        public IList<ProductDTO> GetAllProducts()
        {
            return getProducts(ProductSearchType.None, default(string), default(string));
        }

        public void StoreProduct(ProductDTO product)
        {
            try
            {
                StoreProductRequest request = new StoreProductRequest();
                request.Product = product;
                StoreProductResponse response = Service.StoreProduct(request);
                product.Id = response.Id;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                DeleteProductRequest request = new DeleteProductRequest();
                request.Id = productId;
                DeleteProductResponse response = Service.DeleteProduct(request);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public void GetEstimatedDeliveryTime(int productId, out int unitsAvailable, out int estimatedDeliveryTime)
        {
            unitsAvailable = default(int);
            estimatedDeliveryTime = -1;
            try
            {
                GetEstimatedDeliveryTimeRequest request = new GetEstimatedDeliveryTimeRequest();
                request.Id = productId;
                GetEstimatedDeliveryTimeResponse response = Service.GetEstimatedDeliveryTime(request);
                unitsAvailable = response.UnitsAvailable;
                estimatedDeliveryTime = response.EstimatedDeliveryTime;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        private IList<ProductDTO> getProducts(ProductSearchType searchType, string name, string category)
        {
            try
            {
                GetProductsRequest request = new GetProductsRequest();
                request.SearchType = searchType;
                request.ProductName = name;
                request.Category = category;
                GetProductsResponse response = Service.GetProductsByCriteria(request);
                return response.Products;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new List<ProductDTO>();
            }
        }

        #endregion

        #region Security

        public CurrentUserDTO GetCurrentUser()
        {
            try
            {
                GetCurrentUserResponse response = Service.GetCurrentUser(new GetCurrentUserRequest());
                return response.User;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new CurrentUserDTO();
            }
        }

        public UserDTO GetUserById(int id)
        {
            try
            {
                GetUserRequest request = new GetUserRequest();
                request.Id = id;
                GetUserResponse response = Service.GetUserById(request);
                return response.User;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new UserDTO();
            }
        }

        public IList<UserListDTO> GetUsersByName(string name)
        {
            return getUsers(UserSearchType.ByName, name, default(string));
        }

        public IList<UserListDTO> GetUsersByRole(string role)
        {
            return getUsers(UserSearchType.ByRole, default(string), role);
        }

        public IList<UserListDTO> GetAllUsers()
        {
            return getUsers(UserSearchType.None, default(string), default(string));
        }

        public void StoreUser(UserDTO user)
        {
            try
            {
                StoreUserRequest request = new StoreUserRequest();
                request.User = user;
                StoreUserResponse response = Service.StoreUser(request);
                user.Id = response.Id;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                DeleteUserRequest request = new DeleteUserRequest();
                request.Id = userId;
                DeleteUserResponse response = Service.DeleteUser(request);
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
            }
        }

        public RoleDTO GetRoleById(int id)
        {
            try
            {
                GetRoleRequest request = new GetRoleRequest();
                request.RoleId = id;
                GetRoleResponse response = Service.GetRoleById(request);
                return response.Role;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new RoleDTO();
            }
        }

        public IList<RoleDTO> GetRolesByName(string name)
        {
            return getRoles(RoleSearchType.ByName, name, default(string));
        }

        public IList<RoleDTO> GetAllRoles()
        {
            return getRoles(RoleSearchType.None, default(string), default(string));
        }

        private IList<UserListDTO> getUsers(UserSearchType searchType, string name, string role)
        {
            try
            {
                GetUsersRequest request = new GetUsersRequest();
                request.SearchType = searchType;
                request.Username = name;
                request.Rolename = role;
                GetUsersResponse response = Service.GetUsersByCriteria(request);
                return response.Users;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new List<UserListDTO>();
            }
        }

        private IList<RoleDTO> getRoles(RoleSearchType searchType, string name, string role)
        {
            try
            {
                GetRolesRequest request = new GetRolesRequest();
                request.SearchType = searchType;
                request.Rolename = name;
                request.Rolename = role;
                GetRolesResponse response = Service.GetRolesByCriteria(request);
                return response.Roles;
            }
            catch (Exception ex)
            {
                if (ExceptionPolicy.HandleException(ex, "PL Policy")) throw;
                return new List<RoleDTO>();
            }
        }

        #endregion

        #region IServiceFacade Members

        public void Dispose()
        {
            _service = null;
        }

        #endregion
    }
}