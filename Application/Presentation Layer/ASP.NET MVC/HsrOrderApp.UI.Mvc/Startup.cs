using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HsrOrderApp.UI.Mvc.Startup))]
namespace HsrOrderApp.UI.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
