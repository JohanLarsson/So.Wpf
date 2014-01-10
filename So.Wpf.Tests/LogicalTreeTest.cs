﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using NUnit.Framework;

namespace So.Wpf.Tests
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