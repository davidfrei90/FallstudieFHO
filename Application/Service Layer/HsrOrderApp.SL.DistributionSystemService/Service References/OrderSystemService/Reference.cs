﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HsrOrderApp.SL.DistributionSystemService.OrderSystemService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OrderShippedDTO", Namespace="http://schemas.datacontract.org/2004/07/HsrOrderApp.SL.OrderSystemService")]
    [System.SerializableAttribute()]
    public partial class OrderShippedDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ShippedDateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ShippedDate {
            get {
                return this.ShippedDateField;
            }
            set {
                if ((this.ShippedDateField.Equals(value) != true)) {
                    this.ShippedDateField = value;
                    this.RaisePropertyChanged("ShippedDate");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="order.hsr.ch", ConfigurationName="OrderSystemService.IOrderSystemService")]
    public interface IOrderSystemService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="order.hsr.ch/IOrderSystemService/OrderShippedNotification")]
        void OrderShippedNotification(HsrOrderApp.SL.DistributionSystemService.OrderSystemService.OrderShippedDTO order);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOrderSystemServiceChannel : HsrOrderApp.SL.DistributionSystemService.OrderSystemService.IOrderSystemService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OrderSystemServiceClient : System.ServiceModel.ClientBase<HsrOrderApp.SL.DistributionSystemService.OrderSystemService.IOrderSystemService>, HsrOrderApp.SL.DistributionSystemService.OrderSystemService.IOrderSystemService {
        
        public OrderSystemServiceClient() {
        }
        
        public OrderSystemServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OrderSystemServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderSystemServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OrderSystemServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void OrderShippedNotification(HsrOrderApp.SL.DistributionSystemService.OrderSystemService.OrderShippedDTO order) {
            base.Channel.OrderShippedNotification(order);
        }
    }
}
