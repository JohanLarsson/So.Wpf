using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using JLN.Controls.Misc;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class DependencyPropertyExtTests
    {
        [Test, RequiresSTA]
        public void AncestorsTest()
        {
            var listView = new ListView();
            listView.BeginInit();
            listView.Width = 200;
            var gridView = new GridView();
            listView.View = gridView;
            var gridViewColumn = new System.Windows.Controls.GridViewColumn();
            gridView.Columns.Add(gridViewColumn);
            listView.EndInit();
            var parent = LogicalTreeHelper.GetParent(gridView);
            Assert.NotNull(parent);
            var dependencyObjects = gridViewColumn.LogicalAncestors().ToArray();
        }
    }
    public static class Meh
    {
        public static void Initialize(this FrameworkElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));
            element.UpdateLayout();
        }
    }
}
