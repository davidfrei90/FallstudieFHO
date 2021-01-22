#region

using System;
using System.Configuration;

#endregion

namespace HsrOrderApp.BL.Core.Helpers
{
    public enum ORMapper
    {
        LINQ2SQL,
        EntityFramework
    } ;

    public static class ConfigurationHelper
    {
        private const string DALString = "DataAccessLayerToUse";
        private const string UseMSMQServiceString = "EnableDistributedTransactionToMessageQueueingSystem";

        public static ORMapper OrMapper
        {
            get
            {
                ORMapper mapper;
                string dal = GetAppSetting(DALString);
                if (dal == null)
                    mapper = ORMapper.LINQ2SQL;
                else
                    mapper = (ORMapper) Enum.Parse(typeof (ORMapper), dal, true);
                return mapper;
            }
        }

        public static bool UseMsmqService
        {
            get
            {
                bool retValue = false;
                string useService = GetAppSetting(UseMSMQServiceString);
                bool.TryParse(useService, out retValue);
                return retValue;
            }
        }

        internal static string GetAppSetting(string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);
            return value;
        }
    }
}