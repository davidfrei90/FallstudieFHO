#region

using System;
using System.Configuration;

#endregion

namespace HsrOrderApp.UI.PresentationLogic.Helpers
{
    public static class ConfigurationHelper
    {
        private const string UseServiceLayerString = "UseServiceLayer";
        private const string DeliveryTimeFromStockString = "DeliveryTimeFromStock";

        public static bool UseServiceLayer
        {
            get
            {
                bool retValue = false;
                string useService = GetAppSetting(UseServiceLayerString);
                bool.TryParse(useService, out retValue);
                return retValue;
            }
        }

        public static int DeliveryTimeFromStock
        {
            get
            {
                int deliveryTime = 1;
                try
                {
                    deliveryTime = int.Parse(GetAppSetting(DeliveryTimeFromStockString));
                }catch{}

                return deliveryTime;
            }
        }

        internal static string GetAppSetting(string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);
            return value;
        }
    }
}