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
    internal class CreditRatingConverter : IValueConverter
    {
        #region IValueConverter Members

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CreditRating creditRating = (CreditRating) Enum.Parse(typeof (CreditRating), value.ToString());
            switch (creditRating)
            {
                case CreditRating.Superior:
                    return Strings.CreditRating_Superior;
                case CreditRating.Excellent:
                    return Strings.CreditRating_Excellent;
                case CreditRating.AboveAverage:
                    return Strings.CreditRating_AboveAverage;
                case CreditRating.Average:
                    return Strings.CreditRating_Average;
                case CreditRating.BelowAverage:
                    return Strings.CreditRating_BelowAverage;
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