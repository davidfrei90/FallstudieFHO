#region

using System;

#endregion

namespace HsrOrderApp.UI.PresentationLogic
{
    public enum NavigationResult
    {
        Close,
        Ok,
        Cancel
    } ;

    public static class NavigationService
    {
        private static INavigationService _instance;
        private static object _navigationArgument;

        public static object Argument
        {
            get { return _navigationArgument; }
        }

        public static void AttachNavigator(INavigationService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("service", "Parameter can't be null.");
            }
            _instance = service;
        }

        public static NavigationResult NavigateTo(string url)
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Navigation service not properly initialized: Please ensure a valid navigator service object is passed to NavigatorService's constructor.");
            }

            return _instance.NavigateTo(url);
        }

        public static NavigationResult NavigateTo(string url, object argument)
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Navigation service not properly initialized: Please ensure a valid navigator service object is passed to NavigatorService's constructor.");
            }
            _navigationArgument = argument;
            return NavigateTo(url);
        }
    }
}