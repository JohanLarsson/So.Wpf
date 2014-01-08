using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using JLN.Controls.Misc;

namespace JLN.Controls.AttachedProperties
{
    public static class GridViewColumn
    {
        private static readonly GridLength _defaultValue = new GridLength(0, GridUnitType.Auto);
        private static readonly Dictionary<System.Windows.Controls.GridViewColumn, GridVieColumnWidthInfo> _columns = new Dictionary<System.Windows.Controls.GridViewColumn, GridVieColumnWidthInfo>();

        public static readonly DependencyProperty ColumnWidthProperty =
            DependencyProperty.RegisterAttached("ColumnWidth", typeof(GridLength), typeof(GridViewColumn), new PropertyMetadata(_defaultValue, OnWidthChanged));

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
            if(!((GridLength)e.NewValue).IsStar)
                throw new ArgumentException("ColumnWidth must be starsize");
            var column = (System.Windows.Controls.GridViewColumn)d;
            if (_columns.ContainsKey(column))
                return;
            var info = new GridVieColumnWidthInfo()
            {
                Column = column,
                Width = GetColumnWidth(d)
            };
            _columns.Add(column,info);
            var binding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Selector), 1),
                Converter = new GetAncestorConverter(),
                ConverterParameter = info
            };
            BindingOperations.SetBinding(column, System.Windows.Controls.GridViewColumn.WidthProperty, binding);
        }

        private class GridVieColumnWidthInfo
        {
            private Selector _parent;
            private GridViewColumnCollection _columns1;
            public System.Windows.Controls.GridViewColumn Column { get; set; }
            public GridLength Width { get; set; }

            public Selector Parent
            {
                get { return _parent; }
                set
                {
                    _parent = value;
                    _parent.AddHandler(FrameworkElement.SizeChangedEvent, new SizeChangedEventHandler(PanelSizeChanged));
                    BindingOperations.ClearBinding(Column, System.Windows.Controls.GridViewColumn.WidthProperty);
                }
            }

            public GridViewColumnCollection Columns
            {
                get { return _columns1; }
                set
                {
                    _columns1 = value;
                    foreach (var col in _columns1)
                    {
                        PropertyChangedEventManager.AddHandler(col, (sender, args) => Update(),"ActualWidth");
                    }
                    CollectionChangedEventManager.AddHandler(_columns1, Handler);
                }
            }

            private void Handler(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems != null)
                {
                    foreach (var col in e.NewItems.Cast<System.Windows.Controls.GridViewColumn>())
                    {
                        PropertyChangedEventManager.AddHandler(col, (o, _) => Update(), "ActualWidth");
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (var col in e.OldItems.Cast<System.Windows.Controls.GridViewColumn>())
                    {
                        PropertyChangedEventManager.RemoveHandler(col, (o, _) => Update(), "ActualWidth");
                    }
                }
            }

            private void PanelSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
            {
                Update();
            }

            private void Update()
            {
                var notSet = Columns.Where(x => GetColumnWidth(x) == _defaultValue);
                var sum = notSet.Select(x => x.ActualWidth).Sum();
                var starSized = Columns.Where(x => GetColumnWidth(x).IsStar);
                var totalStarWidth = Parent.ActualWidth - sum;
                var totalStarValue = _columns.Where(x=>starSized.Contains( x.Key)).Sum(x=>x.Value.Width.Value);
                Column.Width =(Width.Value/totalStarValue) *totalStarWidth; 
            }
        }
        private class GetAncestorConverter : IValueConverter
        {
            public object Convert(object value, Type type, object parameter, CultureInfo culture)
            {
                var info = (GridVieColumnWidthInfo)parameter;
                var listView = (ListView)value;
                info.Parent = listView;
                info.Columns = ((GridView)listView.View).Columns;
                return info.Column.ActualWidth;
            }

            public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }
    }
}

