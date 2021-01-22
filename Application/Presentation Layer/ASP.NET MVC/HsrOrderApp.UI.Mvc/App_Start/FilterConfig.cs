using HsrOrderApp.UI.Mvc.Helpers;
using System.Web;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomAuthorizeAttribute());
        }
    }
}
