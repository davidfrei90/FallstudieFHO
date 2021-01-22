using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using HsrOrderApp.BL.Security;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Configuration;

namespace HsrOrderApp.UI.Silverlight.Web
{
    public class Global : System.Web.HttpApplication
    {
            
        protected void Application_Start(object sender, EventArgs e)
        {
            HsrOrderApp.BL.BusinessComponents.DependencyInjection.DependencyInjectionHelper.ConfigProvider = new WebUnityConfigProvider(Server);
        }

        internal class WebUnityConfigProvider : HsrOrderApp.BL.BusinessComponents.DependencyInjection.IUnityConfigProvider
        {
            public WebUnityConfigProvider(HttpServerUtility server)
            {
                this.Server = server;
            }

            public HttpServerUtility Server { get; private set; }

            public Configuration UnityConfiguration
            {
                get
                {
                    var map = new ConfigurationFileMap(Server.MapPath("~/bin/unity.config")); // ToDo: fix the bin folder problem in development server!!!
                    var config = ConfigurationManager.OpenMappedMachineConfiguration(map);
                    return config;
                }
            }
        }

        void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            IPrincipal user = HttpContext.Current.User;


            if(user.Identity.IsAuthenticated && user.Identity.AuthenticationType=="Forms")
            {
                CustomPrincipal principal = null;
                principal = new CustomPrincipal(HttpContext.Current.User.Identity);
                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;
            }

          
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //string cookieName = FormsAuthentication.FormsCookieName;
            //HttpCookie authCookie = Context.Request.Cookies[cookieName];
            //if(null== authCookie)
            //{
            //    // There is no authentication cookie.
            //    return;
            //}
            //FormsAuthenticationTicket authTicket = null;
            //try
            //{
            //    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            //}
            //catch (Exception ex)
            //{
            //    // Log exception details (omitted for simplicity)
            //    return;
            //}
            //if (null == authTicket)
            //{
            //    // Cookie failed to decrypt.
            //    return;
            //}

            //// When the ticket was created, the UserData property was assigned a
            //// pipe delimited string of role names.
            //string[] roles = authTicket.UserData.Split('|');


            //FormsIdentity id = new FormsIdentity( authTicket );
            //// This principal will flow throughout the request.
            //CustomPrincipal principal = new CustomPrincipal(id);
            //// Attach the new principal object to the current HttpContext object
            //Context.User = principal;


         

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

       
    }
}