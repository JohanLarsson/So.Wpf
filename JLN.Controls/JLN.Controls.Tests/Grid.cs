using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JLN.Controls.Tests
{
    /// <summary>
    /// http://samondotnet.blogspot.se/2012/03/wpf-grid-layout-simplified-using-rows.html
    /// </summary>
    public static class Grid
    {
        #region helper methods
        public static IEnumerable<GridLength> Parse(string text)
        {
            if (text.Contains("#"))
            {
                var parts = text.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                var count = int.Parse(parts[1].Trim());
                return Enumerable.Repeat(ParseGridLength(parts[0]), count);
            }
            else
            {
                return new[] { ParseGridLength(text) };
            }
        }
        public static GridLength ParseGridLength(string text)
        {
            text = text.Trim();
            if (text.ToLower() == "auto")
                return GridLength.Auto;
            if (text.Contains("*"))
            {
                var startCount = text.ToCharArray().Count(c => c == '*');
                var pureNumber = text.Replace("*", "");
                var ratio = string.IsNullOrWhiteSpace(pureNumber) ? 1 : double.Parse(pureNumber);
                return new GridLength(startCount * ratio, GridUnitType.Star);
            }
            var pixelsCount = double.Parse(text);
            return new GridLength(pixelsCount, GridUnitType.Pixel);
        }
        #endregion
        #region GridColumnsLayout
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
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, Columns_PropertyChangedCallback));
        public static void Columns_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (oldValue == null || newValue == null)
                return;
            var grid = (System.Windows.Controls.Grid)d;
            if (oldValue != newValue)
            {
                grid.ColumnDefinitions.Clear();
                newValue
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(Parse)
                    .Select(l => new ColumnDefinition { Width = l })
                    .ToList().ForEach(grid.ColumnDefinitions.Add);
            }
        }
        #endregion
        #region Rows
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
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, Rows_PropertyChangedCallback));
        public static void Rows_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (oldValue == null || newValue == null)
                return;
            var grid = (System.Windows.Controls.Grid)d;
            if (oldValue != newValue)
            {
                grid.ColumnDefinitions.Clear();
                newValue
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(Parse)
                    .Select(l => new RowDefinition { Height = l })
                    .ToList().ForEach(grid.RowDefinitions.Add);
            }
        }
        #endregion
        #region Cell
        public static string GetCell(DependencyObject obj)
        {
            return (string)obj.GetValue(CellProperty);
        }
        public static void SetCell(DependencyObject obj, string value)
        {
            obj.SetValue(CellProperty, value);
        }
        // Using a DependencyProperty as the backing store for Cell.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CellProperty =
            DependencyProperty.RegisterAttached("Cell", typeof(string), typeof(UIElement),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure, Cell_PropertyChangedCallback));
        public static void Cell_PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            var oldValue = e.OldValue as string;
            var newValue = e.NewValue as string;
            if (oldValue == null || newValue == null)
                return;
 
            if (oldValue != newValue)
            {
                var rowAndColumn = newValue.Split(new char[] { ' ', ';' });
                var row = int.Parse(rowAndColumn[0]);
                var column = int.Parse(rowAndColumn[1]);
                System.Windows.Controls.Grid.SetRow(element, row);
                System.Windows.Controls.Grid.SetColumn(element, column);
            }
        }
        #endregion
    }
}