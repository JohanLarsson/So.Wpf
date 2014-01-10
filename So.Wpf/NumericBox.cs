using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace So.Wpf
{
    public class NumericBox : TextBox
    {
        public NumericBox()
        {
            AddHandler(MouseWheelEvent, new MouseWheelEventHandler(OnMouseWheel));
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(!IsKeyboardFocused)
                return;
            if(!UpdateWithMouseWheel)
                return;
            Update(e.Delta>0, CultureInfo.InvariantCulture);
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

        public static readonly DependencyProperty UpdateWithMouseWheelProperty =
            DependencyProperty.Register("UpdateWithMouseWheel", typeof (bool), typeof (NumericBox), new PropertyMetadata(false));

        public bool UpdateWithMouseWheel
        {
            get { return (bool) GetValue(UpdateWithMouseWheelProperty); }
            set { SetValue(UpdateWithMouseWheelProperty, value); }
        }


    }
}
