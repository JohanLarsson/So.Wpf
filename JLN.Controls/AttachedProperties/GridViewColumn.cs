using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JLN.Controls.AttachedProperties
{
    public class GridViewColumn : DependencyObject
    {
        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.RegisterAttached("ColumnWidth", typeof(GridLength), typeof(GridViewColumn), new PropertyMetadata(new GridLength(1, GridUnitType.Star),OnWidthChanged));

        private static readonly Dictionary<GridView, Dictionary<System.Windows.Controls.GridViewColumn, GridLength>> GridViews = new Dictionary<GridView, Dictionary<System.Windows.Controls.GridViewColumn, GridLength>>();


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
            //throw new NotImplementedException("message");
            
            //var column = d as System.Windows.Controls.GridViewColumn;
            //var gridView = column.LogicalAncestors().OfType<GridView>().First();
            //var panel = gridView.LogicalAncestors().OfType<Panel>().First();
            //column.
            //if (column != null)
            //{
            //    if (!((GridLength)e.NewValue).IsStar)
            //    {
            //        // Remove scrollviewer
            //        if (GridViews.ContainsKey(column))
            //        {
            //            column.ScrollChanged -= new ScrollChangedEventHandler(ScrollViewer_ScrollChanged);
            //            GridViews.Remove(column);
            //        }
            //    }

            //    if (!string.IsNullOrEmpty((string)e.NewValue))
            //    {
            //        // If group already exists, set scrollposition of new scrollviewer to the scrollposition of the group
            //        if (HorizontalScrollOffsets.Keys.Contains((string)e.NewValue))
            //        {
            //            column.ScrollToHorizontalOffset(HorizontalScrollOffsets[(string)e.NewValue]);
            //        }
            //        else
            //        {
            //            HorizontalScrollOffsets.Add((string)e.NewValue, column.HorizontalOffset);
            //        }

            //        if (VerticalScrollOffsets.Keys.Contains((string)e.NewValue))
            //        {
            //            column.ScrollToVerticalOffset(VerticalScrollOffsets[(string)e.NewValue]);
            //        }
            //        else
            //        {
            //            VerticalScrollOffsets.Add((string)e.NewValue, column.VerticalOffset);
            //        }

            //        // Add scrollviewer
            //        GridViews.Add(column, (string)e.NewValue);
            //        column.ScrollChanged += new ScrollChangedEventHandler(ScrollViewer_ScrollChanged);
            //    }
            //}
        }
    }
}