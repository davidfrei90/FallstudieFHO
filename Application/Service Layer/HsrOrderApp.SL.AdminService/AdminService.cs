#region

using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DtoAdapters;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SL.AdminService
{
    public class AdminService : IAdminService
    {
        #region Order
        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetOrderResponse GetOrderById(GetOrderRequest request)
        {
            GetOrderResponse response = new GetOrderResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();

            response.Order = OrderAdapter.OrderToDto(bc.GetOrderById(request.Id));

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetOrdersResponse GetOrdersByCriteria(GetOrdersRequest request)
        {
            GetOrdersResponse response = new GetOrdersResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();

            IQueryable<Order> orders = bc.GetOrdersByCriteria(request.SearchType, request.CustomerId);
            response.Orders = OrderAdapter.OrdersToListDtos(orders);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public StoreOrderResponse StoreOrder(StoreOrderRequest request)
        {
            StoreOrderResponse response = new StoreOrderResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();

            Order order = OrderAdapter.DtoToOrder(request.Order);
            IEnumerable<ChangeItem> changeItems = OrderAdapter.GetChangeItems(request.Order, order);
            response.Id = bc.StoreOrder(order, changeItems);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public DeleteOrderResponse DeleteOrder(DeleteOrderRequest request)
        {
            DeleteOrderResponse response = new DeleteOrderResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();

            bc.DeleteOrder(request.Id);

            return response;
        }

        #endregion

        #region Customer
        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetCustomerResponse GetCustomerById(GetCustomerRequest request)
        {
            GetCustomerResponse response = new GetCustomerResponse();
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();

            Customer customer = bc.GetCustomerById(request.CustomerId);
            response.Customer = CustomerAdapter.CustomerToDto(customer);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetCustomersResponse GetCustomersByCriteria(GetCustomersRequest request)
        {
            GetCustomersResponse response = new GetCustomersResponse();
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();

            IQueryable<Customer> customers = bc.GetCustomersByCriteria(request.SearchType, request.City, request.CustomerName);
            response.Customers = CustomerAdapter.CustomersToDtos(customers);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public StoreCustomerResponse StoreCustomer(StoreCustomerRequest request)
        {
            StoreCustomerResponse response = new StoreCustomerResponse();
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();

            Customer customer = CustomerAdapter.DtoToCustomer(request.Customer);
            IEnumerable<ChangeItem> changeItems = CustomerAdapter.GetChangeItems(request.Customer, customer);
            response.CustomerId = bc.StoreCustomer(customer, changeItems);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public DeleteCustomerResponse DeleteCustomer(DeleteCustomerRequest request)
        {
            DeleteCustomerResponse response = new DeleteCustomerResponse();
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();

            bc.DeleteCustomer(request.CustomerId);

            return response;
        }

        #endregion

        #region Product
        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetProductResponse GetProductById(GetProductRequest request)
        {
            GetProductResponse response = new GetProductResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            Product product = bc.GetProductById(request.Id);
            response.Product = ProductAdapter.ProductToDto(product);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetProductsResponse GetProductsByCriteria(GetProductsRequest request)
        {
            GetProductsResponse response = new GetProductsResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            IQueryable<Product> products = bc.GetProductsByCriteria(request.SearchType, request.Category, request.ProductName);
            response.Products = ProductAdapter.ProductsToDtos(products);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public StoreProductResponse StoreProduct(StoreProductRequest request)
        {
            StoreProductResponse response = new StoreProductResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            Product product = ProductAdapter.DtoToProduct(request.Product);
            response.Id = bc.StoreProduct(product);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public DeleteProductResponse DeleteProduct(DeleteProductRequest request)
        {
            DeleteProductResponse response = new DeleteProductResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            bc.DeleteProduct(request.Id);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.STAFF)]
        public GetEstimatedDeliveryTimeResponse GetEstimatedDeliveryTime(GetEstimatedDeliveryTimeRequest request)
        {
            GetEstimatedDeliveryTimeResponse response = new GetEstimatedDeliveryTimeResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            int unitsAvailable = default(int);
            int estimatedDeliveryTime = -1;

            bc.GetEstimatedDeliveryTime(request.Id, out unitsAvailable, out estimatedDeliveryTime);
            response.EstimatedDeliveryTime = estimatedDeliveryTime;
            response.UnitsAvailable = unitsAvailable;

            return response;
        }

        #endregion

        #region Security

        public GetCurrentUserResponse GetCurrentUser(GetCurrentUserRequest request)
        {
            GetCurrentUserResponse response = new GetCurrentUserResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            User user = bc.GetUserByName(Thread.CurrentPrincipal.Identity.Name);
            response.User = SecurityAdapter.UserToCurrentUserDTO(user);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public GetUserResponse GetUserById(GetUserRequest request)
        {
            GetUserResponse response = new GetUserResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            User user = bc.GetUserById(request.Id);
            response.User = SecurityAdapter.UserToDTO(user);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public GetUsersResponse GetUsersByCriteria(GetUsersRequest request)
        {
            GetUsersResponse response = new GetUsersResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            IQueryable<User> users = bc.GetUsersByCriteria(request.SearchType, request.Username, request.Rolename);
            response.Users = SecurityAdapter.UsersToDtos(users);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public StoreUserResponse StoreUser(StoreUserRequest request)
        {
            StoreUserResponse response = new StoreUserResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            User user = SecurityAdapter.DtoToUser(request.User);
            IEnumerable<ChangeItem> changeItems = SecurityAdapter.GetChangeItems(request.User);
            response.Id = bc.StoreUser(user, changeItems);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            DeleteUserResponse response = new DeleteUserResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();
            bc.DeleteUser(request.Id);
            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public GetRoleResponse GetRoleById(GetRoleRequest request)
        {
            GetRoleResponse response = new GetRoleResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            Role role = bc.GetRoleById(request.RoleId);
            response.Role = SecurityAdapter.RoleToDTO(role);

            return response;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = Roles.ADMIN)]
        public GetRolesResponse GetRolesByCriteria(GetRolesRequest request)
        {
            GetRolesResponse response = new GetRolesResponse();
            SecurityBusinessComponent bc = DependencyInjectionHelper.GetSecurityBusinessComponent();

            IQueryable<Role> roles = bc.GetRolesByCriteria(request.SearchType, request.Rolename);
            response.Roles = SecurityAdapter.RolesToDTOs(roles);

            return response;
        }

        #endregion
    }
}