using HsrOrderApp.UI.Mvc.Controllers.Base;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Controllers
{
    public class HomeController : HsrOrderAppController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}