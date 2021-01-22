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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HsrOrderApp.UI.Silverlight.ViewModels;
using HsrOrderApp.UI.Silverlight.Views.Login;
using Microsoft.Practices.Prism.Events;


namespace HsrOrderApp.UI.Silverlight
{
    public partial class MainPage : UserControl
    {
        private IEventAggregator _messageBus;
        public MainPage()
        {
            InitializeComponent();
            _messageBus = ((ServiceLocator)App.Current.Resources["ServiceLocator"]).MessageBus;
            ContentFrame.Navigating += (obj, e) => _messageBus.GetEvent<Messages.NavigatingMessage>().Publish(e.Uri);
            ContentFrame.Navigated += (obj, e) => _messageBus.GetEvent<Messages.NavigatedMessage>().Publish(e.Uri);
            VisualStateManager.GoToState(this, "loggedOut", true);
        }

        public Frame ContenetFrame
        {
            get { return ContentFrame; }
        }

        // After the Frame navigates, ensure the HyperlinkButton representing the current page is selected
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
        }



        public void ChangeState(string state)
        {
            VisualStateManager.GoToState(this, state, true);
            Control control = ContentFrame.Content as Control;
            if (control != null)
                VisualStateManager.GoToState(control, state, true);
        }

        // If an error occurs during navigation, show an error window
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }
    }
}