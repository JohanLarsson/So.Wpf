using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MS.Internal.PresentationFramework;

namespace JLN.Controls.AttachedProperties
{
    /// <summary>
    /// http://samondotnet.blogspot.se/2012/03/wpf-grid-layout-simplified-using-rows.html
    /// </summary>
    public static class Grid
    {
        private static readonly GridLengthConverter Converter = new GridLengthConverter();
        private static readonly char[] SplitSeparators = { ' ', ';' };

        internal static IEnumerable<GridLength> ToGridLengths(this object newValue)
        {
            string[] strings =((string)newValue).Split(SplitSeparators);
            return strings.Select(Converter.ConvertFromString).Cast<GridLength>();
        }

        public static string GetColumns(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnsProperty);
        }
        public static void SetColumns(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsProperty, value);
        }
        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns", typeof(string), typeof(System.Windows.Controls.Grid),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, ColumnsUpdated));
        public static void ColumnsUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
                return;
            var grid = (System.Windows.Controls.Grid)d;
            grid.ColumnDefinitions.Clear();
            foreach (var cd in e.NewValue.ToGridLengths()
                                         .Select(x => new ColumnDefinition { Width = x }))
            {
                grid.ColumnDefinitions.Add(cd);
            }
        }

        public static string GetRows(DependencyObject obj)
        {
            return (string)obj.GetValue(RowsProperty);
        }
        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }
        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows", typeof(string), typeof(System.Windows.Controls.Grid),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, RowsUpdated));
        public static void RowsUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
                return;
            var grid = (System.Windows.Controls.Grid)d;
            grid.RowDefinitions.Clear();
            foreach (var rd in e.NewValue.ToGridLengths()
                                         .Select(x => new RowDefinition() { Height = x }))
            {
                grid.RowDefinitions.Add(rd);
            }
        }
        public static string GetCell(DependencyObject obj)
        {
            return (string)obj.GetValue(CellProperty);
        }
        public static void SetCell(DependencyObject obj, string value)
        {
            obj.SetValue(CellProperty, value);
        }

        public static readonly DependencyProperty CellProperty =
            DependencyProperty.RegisterAttached("Cell", typeof(string), typeof(UIElement),
                new FrameworkPropertyMetadata("0 0", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, CellUpdated));


        public static void CellUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (string.Equals(e.OldValue, e.NewValue))
                return;
            var element = (UIElement)d;
            var rowAndColumn = ((string)e.NewValue).Split(SplitSeparators);
            var row = int.Parse(rowAndColumn[0]);
            var column = int.Parse(rowAndColumn[1]);
            System.Windows.Controls.Grid.SetRow(element, row);
            System.Windows.Controls.Grid.SetColumn(element, column);
        }
    }
}