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
    [ValueConversion(typeof (double), typeof (String))]
    public class DoubleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double num = (double) value;
                string retValue = string.Format(Thread.CurrentThread.CurrentUICulture, "{0:n}", value);
                return retValue;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof (double))
            {
                double returnValue = 0;
                if (double.TryParse(value.ToString(), NumberStyles.Float, Thread.CurrentThread.CurrentUICulture, out returnValue))
                    return returnValue;
            }
            return value;
        }

        #endregion
    }
}