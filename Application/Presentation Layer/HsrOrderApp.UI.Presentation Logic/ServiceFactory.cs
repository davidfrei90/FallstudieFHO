#region

using System;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;
using HsrOrderApp.UI.PresentationLogic.Properties;
using HsrOrderApp.UI.PresentationLogic.Helpers;

#endregion

namespace HsrOrderApp.UI.PresentationLogic
{
    internal abstract class ServiceFactory
    {
        public abstract IAdminService CreateBusinessLayerInstance();

        public static ServiceFactory GetCreatorInstance()
        {
            Type businessLayerType = typeof (ServiceLayerCreator);
            if (ConfigurationHelper.UseServiceLayer == false)
                businessLayerType = typeof (BusinessLayerCreator);
            return (ServiceFactory) Activator.CreateInstance(businessLayerType);
        }
    }
}