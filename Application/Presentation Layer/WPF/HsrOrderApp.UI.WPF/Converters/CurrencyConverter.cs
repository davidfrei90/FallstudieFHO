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
    [ValueConversion(typeof (decimal), typeof (String))]
    public class CurrencyConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool useCurrencyFactor = true;
            if (parameter != null)
                bool.TryParse(parameter.ToString(), out useCurrencyFactor);

            if (value is decimal)
            {
                decimal num = ((decimal) value);
                string retValue;
                if (useCurrencyFactor)
                {
                    decimal currencyFactor;
                    if (decimal.TryParse(Strings.CurrencyFactor, out currencyFactor))
                    {
                        num = ((decimal) value)*currencyFactor;
                    }
                    retValue = string.Format(Thread.CurrentThread.CurrentUICulture, "{0:c}", num);
                }
                else
                {
                    retValue = string.Format(CultureInfo.GetCultureInfo("en-US"), "{0:c}", num);
                }
                return retValue;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Currency Input always in USD
            throw new NotSupportedException();

            /*
            if (targetType == typeof(double))
            {
                bool useCurrencyFactor = false;
                bool.TryParse(parameter.ToString(), out useCurrencyFactor);

                double returnValue = 0;
                if (double.TryParse(value.ToString(), NumberStyles.Float, Thread.CurrentThread.CurrentUICulture, out returnValue))
                {
                    if (useCurrencyFactor)
                    {
                        double currencyFactor;
                        if (double.TryParse(Strings.CurrencyFactor, out currencyFactor))
                        {
                            returnValue = ((double) value)/currencyFactor;
                        }
                    }
                    return returnValue;
                }
            }
            return value;
            */
        }

        #endregion
    }
}