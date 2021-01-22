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
    [ValueConversion(typeof (decimal), typeof (String))]
    public class DecimalConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal)
            {
                decimal num = (decimal)value;
                string retValue = string.Format(Thread.CurrentThread.CurrentUICulture, "{0:n}", value);
                return retValue;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof (decimal))
            {
                decimal returnValue = 0;
                if (decimal.TryParse(value.ToString(), NumberStyles.Float, Thread.CurrentThread.CurrentUICulture, out returnValue))
                    return returnValue;
            }
            return value;
        }

        #endregion
    }
}