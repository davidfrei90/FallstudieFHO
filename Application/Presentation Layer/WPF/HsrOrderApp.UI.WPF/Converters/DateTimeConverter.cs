#region

using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;

#endregion

namespace HsrOrderApp.UI.WPF.Converters
{
    /// <summary>
    /// Kurze Datums+Zeit Darstellung.
    /// </summary>
    [ValueConversion(typeof (DateTime?), typeof (String))]
    public class DateTimeConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is DateTime?)
            {
                DateTime? date = (DateTime?) value;
                if (date.HasValue == true)
                {
                    DateTimeFormatInfo info = Thread.CurrentThread.CurrentUICulture.DateTimeFormat;
                    return date.Value.ToString(info.ShortDatePattern + " " + info.ShortTimePattern, Thread.CurrentThread.CurrentUICulture);
                }
                return string.Empty;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof (DateTime?))
            {
                DateTime returnValue = DateTime.MinValue;
                if (DateTime.TryParseExact(value.ToString(), DateTimeFormatInfo.CurrentInfo.ShortDatePattern + " " + DateTimeFormatInfo.CurrentInfo.ShortTimePattern, Thread.CurrentThread.CurrentUICulture, DateTimeStyles.AllowInnerWhite, out returnValue))
                    return returnValue;
                return value;
            }

            return value;
        }

        #endregion
    }
}