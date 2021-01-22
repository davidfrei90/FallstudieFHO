using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HsrOrderApp.UI.Silverlight.CustomerService;
using Microsoft.Practices.Prism.Events;

namespace HsrOrderApp.UI.Silverlight
{
    public class Messages
    {
        public class NavigateMessage : CompositePresentationEvent<Uri>
        {
        }

        public class NavigatingMessage : CompositePresentationEvent<Uri>
        {
        }

        public class NavigatedMessage : CompositePresentationEvent<Uri>
        {
        }

        public class StateChangedMessag: CompositePresentationEvent<String>
        {
            
        }

        public class OrderDetailMessage:CompositePresentationEvent<OrderListDTO>
        {
            
        } 
    }
}
