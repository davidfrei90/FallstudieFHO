using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using HsrOrderApp.SharedLibraries.DTO.Faults;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using Microsoft.Practices.EnterpriseLibrary.Validation.Integration.WCF;
using HsrOrderApp.SharedLibraries.DTO.Requests_Responses;

namespace HsrOrderApp.SharedLibraries.ServiceInterfaces
{
    [DataContract(Namespace = "http://ins.hsr.ch/")]
    public class NotAuthenticatedFault
    {
        [DataMember]
        public String Message { get; set; }
    }


    [ServiceContract(Namespace = "http://ch.hsr.HsrOrderApp")]
    [ValidationBehavior]
   
    public interface ICustomerService
    {
        #region Order
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        [FaultContract(typeof(ValidationFault))]
        //[FaultContract(typeof(NotAuthenticatedFault))]
        GetOrderResponse GetOrderById(GetOrderRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        //[FaultContract(typeof (ValidationFault))]
        GetOrdersResponse GetOrdersByCriteria(GetOrdersRequest request);
        #endregion


        #region Product

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        //[FaultContract(typeof (ValidationFault))]
        GetProductResponse GetProductById(GetProductRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        //[FaultContract(typeof (ValidationFault))]
        GetProductsResponse GetProductByCriteria(GetProductsRequest request);
        #endregion

        #region Customer

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        //[FaultContract(typeof (ValidationFault))]
        StoreCustomerResponse StoreCustomer(StoreCustomerRequest request);

        [OperationContract]
        [FaultContract(typeof (ServiceFault))]
        //[FaultContract(typeof (ValidationFault))]
        GetCustomerResponse GetCustomer();
        #endregion
    }
}
