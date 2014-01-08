using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using JLN.Controls.Misc;
using MS.Internal;
using NUnit.Framework;
using GridViewColumn = JLN.Controls.AttachedProperties.GridViewColumn;

namespace JLN.Controls.Tests
{
    internal class GridViewColumnWitdhTests
    {
        [Test, RequiresSTA]
        public void OneStarSizedTest()
        {
            var view = new GridView();
            var lw = new ListView();
            var gvc = new System.Windows.Controls.GridViewColumn();
            lw.View = view;
            var dependencyObjects = view.LogicalAncestors().ToArray();

        }

        [Test, RequiresSTA]
        public void HackGetParentTest()
        {
            var lw = new ListView();
            var view = new GridView();
            var gvc = new System.Windows.Controls.GridViewColumn();
            view.Columns.Add(gvc);
            lw.View = view;
            var ancestor = new Ancestor<ListView>();

            var binding = new Binding
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ListView), 1),
                Converter = new GetAncestorConverter<ListView>(),
                ConverterParameter = ancestor
            };
            BindingOperations.SetBinding(gvc, System.Windows.Controls.GridViewColumn.WidthProperty, binding);
            lw.Items.Add(DateTime.Now);
            ResolveBinding(lw);
            Assert.AreEqual(lw, ancestor.Instance);
        }

        private void ResolveBinding(FrameworkElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));
            element.UpdateLayout();
        }
    }
    public class GetAncestorConverter<T> : IValueConverter
    {
        public object Convert(object value, Type type, object parameter, CultureInfo culture)
        {
            var ancestor = (Ancestor<T>)parameter;
            ancestor.Instance = (T)value;
            return null;
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class Ancestor<T>
    {
        public T Instance { get; set; }
    }
}
