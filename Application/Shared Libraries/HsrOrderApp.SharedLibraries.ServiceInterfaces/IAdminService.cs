#region

using System.ServiceModel;
using HsrOrderApp.SharedLibraries.DTO.Faults;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;

#endregion

namespace HsrOrderApp.SharedLibraries.ServiceInterfaces
{
    [ServiceContract(Namespace = "http://ch.hsr.HsrOrderApp")]
    [ValidationBehavior]
    [ExceptionShielding]
    public interface IAdminService
    {
        #region Order

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetOrderResponse GetOrderById(GetOrderRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetOrdersResponse GetOrdersByCriteria(GetOrdersRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        StoreOrderResponse StoreOrder(StoreOrderRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        DeleteOrderResponse DeleteOrder(DeleteOrderRequest request);

        #endregion

        #region Customer

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetCustomerResponse GetCustomerById(GetCustomerRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetCustomersResponse GetCustomersByCriteria(GetCustomersRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        StoreCustomerResponse StoreCustomer(StoreCustomerRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        DeleteCustomerResponse DeleteCustomer(DeleteCustomerRequest request);

        #endregion


        #region Product

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetProductResponse GetProductById(GetProductRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetProductsResponse GetProductsByCriteria(GetProductsRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        StoreProductResponse StoreProduct(StoreProductRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        DeleteProductResponse DeleteProduct(DeleteProductRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetEstimatedDeliveryTimeResponse GetEstimatedDeliveryTime(GetEstimatedDeliveryTimeRequest request);

        #endregion

        #region Security 

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetCurrentUserResponse GetCurrentUser(GetCurrentUserRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetUserResponse GetUserById(GetUserRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetUsersResponse GetUsersByCriteria(GetUsersRequest request);


        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        StoreUserResponse StoreUser(StoreUserRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        DeleteUserResponse DeleteUser(DeleteUserRequest request);


        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetRoleResponse GetRoleById(GetRoleRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        [FaultContract(typeof (ValidationFault))]
        GetRolesResponse GetRolesByCriteria(GetRolesRequest request);

        #endregion
    }
}