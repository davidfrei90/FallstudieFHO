#region

using System.ServiceModel;
using HsrOrderApp.SharedLibraries.ServiceInterfaces;

#endregion

namespace HsrOrderApp.UI.PresentationLogic
{
    internal class ServiceLayerCreator : ServiceFactory
    {
        public override IAdminService CreateBusinessLayerInstance()
        {
            ChannelFactory<IAdminService> channelFactory = new ChannelFactory<IAdminService>("AdminService");
            return channelFactory.CreateChannel();
        }
    }
}