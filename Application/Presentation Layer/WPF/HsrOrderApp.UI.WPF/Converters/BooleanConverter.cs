#region

using System;
using System.Globalization;
using System.Threading;
using System.Windows.Data;
using HsrOrderApp.UI.WPF.Properties;

#endregion

namespace HsrOrderApp.UI.WPF.Converters
{
    /// <summary>
    /// Kurze Datums+Zeit Darstellung.
    /// </summary>
    [ValueConversion(typeof (bool), typeof (String))]
    public class BooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                bool flag = (bool) value;
                return flag ? Strings.True : Strings.False;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           throw new NotSupportedException();
        }

        #endregion
    }
}