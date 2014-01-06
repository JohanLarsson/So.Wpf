using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace JLN.Controls
{
    public class DoubleBox : TextBox
    {
        public DoubleBox()
        {
            this.AddHandler(MouseWheelEvent, new RoutedEventHandler(OnMouseWheel));
        }

        private void OnMouseWheel(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
        }

        internal void Update(bool increase, CultureInfo culture)
        {
            double d;
            if (!double.TryParse(Text, NumberStyles.Float, culture.NumberFormat, out d))
                return;
            var decimalIndex = Text.IndexOf(culture.NumberFormat.NumberDecimalSeparator, System.StringComparison.Ordinal);
            if (decimalIndex < 0)
                decimalIndex = Text.Length;
            var caretIndex = CaretIndex;
            double digit;
            if (caretIndex <= decimalIndex)
            {
                digit = (decimalIndex - caretIndex);
            }
            //else if (caretIndex == (decimalIndex + 1))
            //{
            //    digit = 0;
            //}
            else
            {
                digit = (decimalIndex-caretIndex+1);
            }
            int sign = increase ? 1 : -1;
            var oldLength = Text.Length;
            var newValue = d + sign * Math.Pow(10, digit);
            var digits = oldLength - decimalIndex - 1;
            Text = newValue.ToString("f" + (digits < 0 ? 0 : digits), culture);
            if (Math.Sign(newValue) * d < 0)
            {
                if (newValue < d)
                    caretIndex++;
                else
                    caretIndex--;
            }
            else
            {
                if (oldLength < Text.Length)
                {
                    caretIndex++;
                }
                else if (Text.Length > oldLength)
                {
                    caretIndex--;
                }
            }
            CaretIndex = caretIndex;
        }
    }
}
