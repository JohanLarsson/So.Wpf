using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MS.Internal.PresentationFramework;

namespace So.Wpf.AttachedProperties
{
    /// <summary>
    /// http://samondotnet.blogspot.se/2012/03/wpf-grid-layout-simplified-using-rows.html
    /// </summary>
    public static class Grid
    {
        private static readonly GridLengthConverter Converter = new GridLengthConverter();
        private static readonly char[] SplitSeparators = { ' ', ';' };
        internal static IEnumerable<GridLength> ToGridLengths(object newValue)
        {
            if (newValue == null)
                return Enumerable.Empty<GridLength>();
            string[] strings = ((string)newValue).Split(SplitSeparators);
            return strings.Select(Converter.ConvertFromString).Cast<GridLength>();
        }
        public static string GetColumns(System.Windows.Controls.Grid grid)
        {
            return (string)grid.GetValue(ColumnsProperty);
        }
        public static void SetColumns(System.Windows.Controls.Grid grid, string value)
        {
            grid.SetValue(ColumnsProperty, value);
        }
        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns", typeof(string), typeof(Grid),
                new FrameworkPropertyMetadata("*", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, ColumnsUpdated));
        public static void ColumnsUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
                return;
            var grid = (System.Windows.Controls.Grid)d;
            grid.ColumnDefinitions.Clear();
            foreach (var cd in ToGridLengths(e.NewValue).Select(x => new ColumnDefinition { Width = x }))
            {
                grid.ColumnDefinitions.Add(cd);
            }
        }
        public static string GetRows(System.Windows.Controls.Grid grid)
        {
            return (string)grid.GetValue(RowsProperty);
        }
        public static void SetRows(System.Windows.Controls.Grid grid, string value)
        {
            grid.SetValue(RowsProperty, value);
        }
        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows", typeof(string), typeof(Grid),
                new FrameworkPropertyMetadata("*", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, RowsUpdated));
        public static void RowsUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
                return;
            var grid = (System.Windows.Controls.Grid)d;
            grid.RowDefinitions.Clear();
            foreach (var rd in ToGridLengths(e.NewValue).Select(x => new RowDefinition() { Height = x }))
            {
                grid.RowDefinitions.Add(rd);
            }
        }
        public static string GetCell(UIElement obj)
        {
            return (string)obj.GetValue(CellProperty);
        }
        public static void SetCell(UIElement obj, string value)
        {
            obj.SetValue(CellProperty, value);
        }

        public static readonly DependencyProperty CellProperty =
            DependencyProperty.RegisterAttached("Cell", typeof(string), typeof(Grid),
                new FrameworkPropertyMetadata("0 0", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, CellUpdated));


        public static void CellUpdated(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (string.Equals(e.OldValue, e.NewValue))
                return;
            var element = (UIElement)d;
            //var grid = (System.Windows.Controls.Grid)element.VisualAncestors().OfType<System.Windows.Controls.Panel>().First();
            var rowAndColumn = ((string)e.NewValue).Split(SplitSeparators);
            var row = int.Parse(rowAndColumn[0]);
            var column = int.Parse(rowAndColumn[1]);
            //if (row >= grid.RowDefinitions.Count)
            //    throw new FormatException("Grid.Row cannot be > RowDefinitions.Count");
            //if (column >= grid.ColumnDefinitions.Count)
            //    throw new FormatException("Grid.Colunn cannot be > ColumnDefinitions.Count");
            System.Windows.Controls.Grid.SetRow(element, row);
            System.Windows.Controls.Grid.SetColumn(element, column);
        }
    }
}