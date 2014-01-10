using System;
using System.Globalization;
using System.Windows.Data;

namespace So.Wpf.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = System.Convert.ToDouble(value);
            double p = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            return d * p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
