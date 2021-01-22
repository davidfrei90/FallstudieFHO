#region

using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Threading;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.PresentationLogic.ExceptionHandlers;
using HsrOrderApp.UI.WPF;
using HsrOrderApp.UI.WPF.Navigation;
using HsrOrderApp.UI.WPF.ViewModels;

#endregion

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string _defaultCulture = "de-CH"; // use default culture de-CH
        private ExceptionHandler _exHandler;

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            HandleCulture(e);
            _exHandler = ExceptionHandler.GetInstance();
            this.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            _exHandler.ExceptionOccured += new ExceptionOccuredDelegate(_exHandler_ExceptionOccured);


            base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            window.DataContext = viewModel;
            NavigationService.AttachNavigator(new ApplicationNavigationService(viewModel));
            window.Show();
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = _exHandler.HandleException(e.Exception);
        }

        private void _exHandler_ExceptionOccured(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void HandleCulture(StartupEventArgs e)
        {
            // Application is running
            // Process command line args

            // FOR TESTING ONLY
            //_defaultCulture = "en-US";

            for (int i = 0; i != e.Args.Length; ++i)
            {
                if (e.Args[i].StartsWith("/culture="))
                {
                    _defaultCulture = e.Args[i].Replace("/culture=", "");
                    break;
                }
            }

            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(_defaultCulture);
            }
            catch (ArgumentException)
            {
                // Start application in default culture
            }

            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof (FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.GetCultureInfo(_defaultCulture).IetfLanguageTag)));
        }
    }
}