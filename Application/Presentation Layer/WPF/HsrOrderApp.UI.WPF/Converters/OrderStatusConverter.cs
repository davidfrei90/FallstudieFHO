#region

using System;
using System.Globalization;
using System.Windows.Data;
using HsrOrderApp.SharedLibraries.SharedEnums;
using HsrOrderApp.UI.WPF.Properties;

#endregion

namespace HsrOrderApp.UI.WPF.Converters
{
    [ValueConversion(typeof (string), typeof (string))]
    internal class OrderStatusConverter : IValueConverter
    {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            OrderStatus orderStatus = (OrderStatus) Enum.Parse(typeof (OrderStatus), value.ToString());
            switch (orderStatus)
            {
                case OrderStatus.Draft:
                    return Strings.OrderStatus_Draft;
                case OrderStatus.Ordered:
                    return Strings.OrderStatus_Ordered;
                case OrderStatus.Shipped:
                    return Strings.OrderStatus_Shipped;
            }
            return value;
        }


        object IValueConverter.ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack not supported.");
        }

        #endregion
    }
}