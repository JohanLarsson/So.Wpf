using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using NUnit.Framework;
using Grid = JLN.Controls.AttachedProperties.Grid;

namespace JLN.Controls.Tests
{
    class WpfTreesTests
    {
        [Test, RequiresSTA]
        public void VisualTreeParentTest()
        {
            var grid = new System.Windows.Controls.Grid();
            var button = new Button();
            grid.Children.Add(button);
            var parent = VisualTreeHelper.GetParent(button);
            Assert.AreEqual(grid,parent);
        }

        [Test, RequiresSTA]
        public void LogicalTreeTest()
        {
            var grid = new System.Windows.Controls.Grid();
            var button = new Button();
            grid.Children.Add(button);
            var parent = LogicalTreeHelper.GetParent(button);
            Assert.AreEqual(grid, parent);
        }

        [Test, RequiresSTA]
        public void LogicalTreeTest2()
        {
            var gridView = new GridView();
            var gridViewColumn = new System.Windows.Controls.GridViewColumn();
            gridView.Columns.Add(gridViewColumn);
            var parent = LogicalTreeHelper.GetParent(gridViewColumn);
            Assert.AreEqual(gridView, parent); // Fail
        }
    }
}
