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

        internal void Update(bool increase)
        {
            double d;
            if(!double.TryParse(Text,out d))
                return;
            var decimalIndex = Text.IndexOf(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, System.StringComparison.Ordinal);
            if (decimalIndex < 0)
                decimalIndex = Text.Length;
            var caretIndex =CaretIndex;
            double digit;
            if (caretIndex >= decimalIndex)
            {
                digit = (caretIndex - decimalIndex - 1);

            }
            else if (caretIndex == (decimalIndex + 1))
            {
                digit = 0;
            }
            else
            {
                digit = (caretIndex - decimalIndex);
            }
            Text = (d + Math.Pow(10,digit)).ToString();
        }
    }
}
