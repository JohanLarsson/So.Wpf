namespace So.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var d = System.Convert.ToDouble(value);
            double p = System.Convert.ToDouble(parameter, culture);
            return d * p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
