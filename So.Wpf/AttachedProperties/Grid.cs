namespace So.Wpf.AttachedProperties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Misc;

    /// <summary>
    /// http://samondotnet.blogspot.se/2012/03/wpf-grid-layout-simplified-using-rows.html
    /// </summary>
    public static class Grid
    {
        public static readonly DependencyProperty RowsProperty = DependencyProperty.RegisterAttached(
            "Rows",
            typeof(string),
            typeof(Grid),
            new FrameworkPropertyMetadata(
                "*",
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                RowsUpdated));

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.RegisterAttached(
            "Columns",
            typeof(string),
            typeof(Grid),
            new FrameworkPropertyMetadata(
                "*",
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure,
                ColumnsUpdated));

        public static readonly DependencyProperty CellProperty = DependencyProperty.RegisterAttached(
            "Cell",
            typeof(string),
            typeof(Grid),
            new FrameworkPropertyMetadata(
                "0 0",
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                CellUpdated));

        private static readonly GridLengthConverter Converter = new GridLengthConverter();

        private static readonly char[] SplitSeparators = { ' ', ';', ',' };
        public static string GetRows(System.Windows.Controls.Grid grid)
        {
            return (string)grid.GetValue(RowsProperty);
        }
        public static void SetRows(System.Windows.Controls.Grid grid, string value)
        {
            grid.SetValue(RowsProperty, value);
        }
        public static string GetColumns(System.Windows.Controls.Grid grid)
        {
            return (string)grid.GetValue(ColumnsProperty);
        }
        public static void SetColumns(System.Windows.Controls.Grid grid, string value)
        {
            grid.SetValue(ColumnsProperty, value);
        }
        public static string GetCell(UIElement obj)
        {
            return (string)obj.GetValue(CellProperty);
        }
        public static void SetCell(UIElement obj, string value)
        {
            obj.SetValue(CellProperty, value);
        }
        internal static IEnumerable<GridLength> ToGridLengths(string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
            {
                return Enumerable.Empty<GridLength>();
            }
            string[] strings = newValue.Split(SplitSeparators, StringSplitOptions.RemoveEmptyEntries);
            return strings.Select(Converter.ConvertFromString).Cast<GridLength>();
        }
        private static void RowsUpdated(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
            {
                return;
            }
            var grid = (System.Windows.Controls.Grid)o;
            grid.RowDefinitions.Clear();
            foreach (var rd in ToGridLengths((string)e.NewValue).Select(x => new RowDefinition() { Height = x }))
            {
                grid.RowDefinitions.Add(rd);
            }
        }
        private static void ColumnsUpdated(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (Equals(e.NewValue, e.OldValue))
            {
                return;
            }
            var grid = (System.Windows.Controls.Grid)o;
            grid.ColumnDefinitions.Clear();
            foreach (var cd in ToGridLengths((string)e.NewValue).Select(x => new ColumnDefinition { Width = x }))
            {
                grid.ColumnDefinitions.Add(cd);
            }
        }
        private static void CellUpdated(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (string.Equals(e.OldValue, e.NewValue))
            {
                return;
            }
            var element = (UIElement)o;

            var rowAndColumn = ((string)e.NewValue).Split(SplitSeparators, StringSplitOptions.RemoveEmptyEntries);
            var row = int.Parse(rowAndColumn[0]);
            var column = int.Parse(rowAndColumn[1]);

            System.Windows.Controls.Grid.SetRow(element, row);
            System.Windows.Controls.Grid.SetColumn(element, column);

            var grid = (System.Windows.Controls.Grid)element.VisualAncestors().OfType<Panel>().First();
            if (row > 0 && row >= grid.RowDefinitions.Count)
            {
                throw new ArgumentOutOfRangeException("e", row, string.Format("Grid.Row cannot be > Grid.RowDefinitions.Count ({0})", grid.RowDefinitions.Count));
            }
            if (column > 0 && column >= grid.ColumnDefinitions.Count)
            {
                throw new ArgumentOutOfRangeException("e", column, string.Format("Grid.Colunn cannot be > Grid.ColumnDefinitions.Count ({0})", grid.ColumnDefinitions.Count));
            }
        }
    }
}