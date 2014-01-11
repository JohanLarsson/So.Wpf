namespace So.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;
    using Misc;
    public class NumberToColorConverter : IValueConverter
    {
        public NumberToColorConverter()
        {
            MaxValue = 100;
        }
        public double MaxValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Colors.Transparent;
            }

            var percent = (double)value;
            var min = Math.Min(MaxValue, Math.Abs(percent));
            double saturation = min / MaxValue;
            double hue = (percent < 0) ? 0 : 0.333333;
            Color color = ColorExt.ColorFromHsl(hue, saturation, 0.5);
            return new SolidColorBrush(color);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
