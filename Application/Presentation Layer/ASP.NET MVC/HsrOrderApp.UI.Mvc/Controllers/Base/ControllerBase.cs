using HsrOrderApp.UI.Mvc.Helpers;
using HsrOrderApp.UI.Mvc.Models.Base;
using HsrOrderApp.UI.PresentationLogic;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HsrOrderApp.UI.Mvc.Controllers.Base
{
    public abstract class HsrOrderAppController : Controller, IDisposable
    {
        #region Fields

        private IServiceFacade _service;

        #endregion

        #region Properties

        public IServiceFacade Service
        {
            get
            {
                if (this._service == null)
                {
                    this._service = ServiceFacade.GetInstance();
                }
                return this._service;
            }
            set { this._service = value; }
        }

        #endregion

        #region TempData

        protected T GetViewModelFromTempData<T>(string keySuffix) where T : ViewModelBase
            => GetViewModelFromTempDataInternal<T>($"{typeof(T).FullName}_{keySuffix}");

        protected T GetViewModelFromTempData<T>() where T : ViewModelBase
            => GetViewModelFromTempDataInternal<T>($"{typeof(T).FullName}_{GetType().FullName}");

        private T GetViewModelFromTempDataInternal<T>(string key)
        {
            T vm;
            if (TempData.TryGetValue<T>(key, out vm))
            {
                return vm;
            }
            return default(T);
        }

        protected void StoreViewModelToTempData<T>(T vm, string keySuffix) where T : ViewModelBase
            => StoreViewModelToTempDataInternal<T>(vm, $"{typeof(T).FullName}_{keySuffix}");

        protected void StoreViewModelToTempData<T>(T vm) where T : ViewModelBase
            => StoreViewModelToTempDataInternal<T>(vm, $"{typeof(T).FullName}_{GetType().FullName}");

        private void StoreViewModelToTempDataInternal<T>(T vm, string key) where T : ViewModelBase
            => TempData.AddOrReplace(key, vm);

        protected void RemoveViewModelFromTempData<T>(string keySuffix) where T : ViewModelBase
            => TempData.Remove($"{typeof(T).FullName}_{keySuffix}");

        protected void RemoveViewModelFromTempData<T>() where T : ViewModelBase
             => TempData.Remove($"{typeof(T).FullName}_{GetType().FullName}");

        #endregion

        #region Navigation

        protected string CurrentControllerName => GetRouteDataValue(GetCurrentRouteData(), "controller");
        protected string CurrentActionName => GetRouteDataValue(GetCurrentRouteData(), "action");
        protected string CurrentParameterId => GetRouteDataValue(GetCurrentRouteData(), "id");
        protected string ReferrerControllerName => GetRouteDataValue(GetReferrerRouteData(), "controller");
        protected string ReferrerActionName => GetRouteDataValue(GetReferrerRouteData(), "action");
        protected string ReferrerParameterId => GetRouteDataValue(GetReferrerRouteData(), "id");
        protected RedirectResult RedirectToReferrer() => Redirect(Request.UrlReferrer.ToString());

        private string GetRouteDataValue(RouteData rd, string key)
            => rd != null && rd.Values.ContainsKey(key) ? rd.Values[key].ToString() : string.Empty;

        private RouteData GetCurrentRouteData() => Request.RequestContext.RouteData;
        private RouteData GetReferrerRouteData()
        {
            if (Request.UrlReferrer == null) { return null; }

            var url = Request.UrlReferrer.ToString();
            var request = new HttpRequest(null, url, null);
            var response = new HttpResponse(new System.IO.StringWriter());
            var httpContext = new HttpContext(request, response);
            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            return routeData;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _service?.Dispose();
        }

        #endregion
    }
}