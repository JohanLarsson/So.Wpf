using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using JLN.Controls.Misc;

namespace JLN.Controls.AttachedProperties
{
    public class GridViewColumn : DependencyObject
    {
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.RegisterAttached("ColumnWidth", typeof(GridLength), typeof(GridViewColumn), new PropertyMetadata(new GridLength(1, GridUnitType.Star),OnWidthChanged));

        /// <summary>
        /// Sets the value of the attached property ScrollGroup.
        /// </summary>
        /// <param name="obj">Object on which the property should be applied.</param>
        /// <param name="gridLength">Value of the property.</param>
        public static void SetColumnWidth(DependencyObject obj, GridLength gridLength)
        {
            obj.SetValue(ColumnWidthProperty, gridLength);
        }

        /// <summary>
        /// Gets the value of the attached property ScrollGroup.
        /// </summary>
        /// <param name="obj">Object for which the property should be read.</param>
        /// <returns>Value of the property StartTime</returns>
        public static GridLength GetColumnWidth(DependencyObject obj)
        {
            return (GridLength)obj.GetValue(ColumnWidthProperty);
        }

        /// <summary>
        /// Occurs, when the ScrollGroupProperty has changed.
        /// </summary>
        /// <param name="d">The DependencyObject on which the property has changed value.</param>
        /// <param name="e">Event data that is issued by any event that tracks changes to the effective value of this property.</param>
        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var column = (System.Windows.Controls.GridViewColumn)d;
            var binding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Selector), 1),
                Converter = new GridViewColumnWidthConverter(),
                ConverterParameter = GetColumnWidth(d)
            };
            BindingOperations.SetBinding(column,System.Windows.Controls.GridViewColumn.WidthProperty, binding);
        }
    }

    public class GridViewColumnWidthConverter : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            ListView lw = (ListView)value;
            GridView gw =(GridView) lw.View;
            var converter = new GridLengthConverter();
            //var gl =(GridLength) converter.ConvertFromString((string)parameter);
            double sum = gw.Columns.Select(x => x.ActualWidth).Sum();
            return lw.ActualWidth - sum;
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

