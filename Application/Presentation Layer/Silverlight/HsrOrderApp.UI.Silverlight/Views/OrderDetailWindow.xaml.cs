using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HsrOrderApp.UI.Silverlight.ViewModels;

namespace HsrOrderApp.UI.Silverlight.Views
{
    public partial class OrderDetailWindow : ChildWindow
    {
        public OrderDetailWindow()
        {
            InitializeComponent();
        }
        
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            OrderDetailViewModel orderDetailWindow = this.DataContext as OrderDetailViewModel;
            orderDetailWindow.Unscribe();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

