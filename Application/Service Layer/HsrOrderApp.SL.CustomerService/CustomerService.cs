using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DtoAdapters;
using HsrOrderApp.BL.Security;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.SL.AdminService;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SL.CustomerService
{
   
 
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    class CustomerService : ICustomerService
    {
        private AdminService _adminService;
        public CustomerService()
        {
           
            Thread.CurrentPrincipal = HttpContext.Current.User;
            
        }


        public StoreCustomerResponse StoreCustomer(StoreCustomerRequest request)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            StoreCustomerResponse response = new StoreCustomerResponse();
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();
            Customer customer = CustomerAdapter.DtoToCustomer(request.Customer);
            IEnumerable<ChangeItem> changeItems = CustomerAdapter.GetChangeItems(request.Customer, customer);
            response.CustomerId = bc.StoreCustomer(customer, changeItems);

            return response;
        }


        public GetOrderResponse GetOrderById(GetOrderRequest request)
        {   
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            GetOrderResponse response = new GetOrderResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();

            response.Order = OrderAdapter.OrderToDto(bc.GetOrderById(request.Id));
           
            return response;
        }

        public GetOrdersResponse GetOrdersByCriteria(GetOrdersRequest request)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            GetOrdersResponse response = new GetOrdersResponse();
            OrderBusinessComponent bc = DependencyInjectionHelper.GetOrderBusinessComponent();            
            IQueryable<Order> orders = bc.GetOrdersByCriteria(request.SearchType, request.CustomerId);
            response.Orders = OrderAdapter.OrdersToListDtos(orders);

            return response; ;
        }

        public GetProductResponse GetProductById(GetProductRequest request)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            GetProductResponse response = new GetProductResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();
            Product product = bc.GetProductById(request.Id);
            response.Product = ProductAdapter.ProductToDto(product);

            return response;
        }

        public GetProductsResponse GetProductByCriteria(GetProductsRequest request)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            GetProductsResponse response = new GetProductsResponse();
            ProductBusinessComponent bc = DependencyInjectionHelper.GetProductBusinessComponent();

            IQueryable<Product> products = bc.GetProductsByCriteria(request.SearchType, request.Category, request.ProductName);
            response.Products = ProductAdapter.ProductsToDtos(products);

            return response;
        }
      
        public GetCustomerResponse GetCustomer()
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                throw new FaultException<NotAuthenticatedFault>(new NotAuthenticatedFault());
            GetCustomerResponse response = new GetCustomerResponse();
            SecurityBusinessComponent sc = DependencyInjectionHelper.GetSecurityBusinessComponent();
            HsrOrderApp.BL.Security.CustomPrincipal principal = HttpContext.Current.User as CustomPrincipal;
            if (principal == null)
                return response ;
            User user = sc.GetUserById(principal.User.UserId);
            CustomerBusinessComponent bc = DependencyInjectionHelper.GetCustomerBusinessComponent();
            Customer customer = bc.GetCustomerById(user.Customer.CustomerId);
            response.Customer = CustomerAdapter.CustomerToDto(customer);

            return response;
        }
    }
}

