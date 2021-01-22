namespace System.Data.Linq
{
    internal static class TimeStampConverter
    {
        public static ulong ToUlong(this Binary binary)
        {
            return BitConverter.ToUInt64(binary.ToArray(), 0);
        }

        public static Binary ToTimestamp(this ulong value)
        {
            if (value == 0)
            {
                return null;
            }
            return new Binary(BitConverter.GetBytes(value));
        }
    }
}