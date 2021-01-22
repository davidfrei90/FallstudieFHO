using System;
using System.IdentityModel.Policy;
using System.Threading;
using HsrOrderApp.BL.Security;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.SL.AdminService;
using System.Security.Principal;

namespace HsrOrderApp.UI.PresentationLogic
{
    internal class BusinessLayerCreator : ServiceFactory
    {
        public override IAdminService CreateBusinessLayerInstance()
        {
            // Setzt den CurrentPrincipal für jeden neu erstellten Thread immer auf den Windows-User.
            // Das garantiert das Funktionieren der Security-Richtlinen (Role-based Security).
            AppDomain.CurrentDomain.SetThreadPrincipal(new CustomPrincipal(WindowsIdentity.GetCurrent()));
            
            return new AdminService();
        }
    }
}