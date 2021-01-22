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
using System.Windows.Navigation;

namespace HsrOrderApp.UI.SilverlightApp.Views.Login
{
    public partial class LoginStatus : Page
    {
        public LoginStatus()
        {
            InitializeComponent();
            VisualStateManager.GoToState(this, "loggedOut", true);
            

        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var loginWindow = new n();
        //    loginWindow.LoginCompleted += new EventHandler(loginWindow_LoginCompleted);
        //    loginWindow.Show();

        //}

        void loginWindow_LoginCompleted(object sender, EventArgs e)
        {
            UpdateState("loggedIn", new List<Control>() { this });

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            App.AuthenticationServiceClient.LogoutCompleted += AuthenticationServiceClient_LogoutCompleted;
            App.AuthenticationServiceClient.LogoutAsync();
            UpdateState("loggedOut", new List<Control>() { this });
        }

        void AuthenticationServiceClient_LogoutCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if(!e.Cancelled)
            {
                UpdateState("loggedOut", new List<Control>() { this });
            }
        }

        private void UpdateState(string state,List<Control> controls)
        {
            
            //VisualStateManager.GoToState(this, state, true);

            MainPage mp = App.Current.RootVisual as MainPage;   
            if (mp != null)         
                VisualStateManager.GoToState(mp, state, true);
        }
    }
}
