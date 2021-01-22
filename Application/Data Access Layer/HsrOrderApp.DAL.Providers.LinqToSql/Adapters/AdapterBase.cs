#region

using System.Data.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal abstract class AdapterBase
    {
        public static ulong GetVersionAsUlong(Binary binary)
        {
            return binary.ToUlong();
        }
    }
}