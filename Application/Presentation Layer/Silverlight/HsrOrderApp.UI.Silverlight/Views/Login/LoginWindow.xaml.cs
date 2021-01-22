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

namespace HsrOrderApp.UI.Silverlight.Views.Login
{
    /// <summary>
    /// Use this Control when an login Window is needed
    /// </summary>
    public partial class LoginWindow :Page
    {
        /// <summary>
        /// Occurs when [login completed].
        /// </summary>
        public event EventHandler LoginCompleted;

        public LoginWindow()
        {
            InitializeComponent();
         
           
        }

        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticationServiceClient.LoginCompleted += AuthenticationServiceClient_LoginCompleted;
            App.AuthenticationServiceClient.LoginAsync(Username.Text, Password.Password, null, false);
            //Changes the Cursor Apperance so that the user gets a feedback.
            this.Cursor = Cursors.Wait;

        }

        /// <summary>
        /// Handles the LoginCompleted event
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="HsrOrderApp.UI.Silverlight.AuthenticationService.LoginCompletedEventArgs"/> instance containing the event data.</param>
        void AuthenticationServiceClient_LoginCompleted(object sender, AuthenticationService.LoginCompletedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
          

        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            //this.DialogResult = false;
        }

       
    }
}

