#region

using System;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework
{
    internal static class TimeStampConverter
    {
        public static ulong ToUlong(this byte[] binary)
        {
            return BitConverter.ToUInt64(binary, 0);
        }

        public static byte[] ToTimestamp(this ulong value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}