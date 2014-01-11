namespace So.Wpf.Tests
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using NUnit.Framework;
    using Misc;
    public class DependencyObjectExtTests
    {
        [Test, RequiresSTA]
        public void VisualAncestorsTest()
        {
            var grid = new System.Windows.Controls.Grid();
            var button = new Button();
            grid.Children.Add(button);
            var parent = VisualTreeHelper.GetParent(button);
            Assert.AreEqual(grid, parent);
            Assert.AreEqual(grid, button.VisualAncestors().Single());
        }

        [Test, RequiresSTA]
        public void LogicalAncestorsTest()
        {
            var grid = new System.Windows.Controls.Grid();
            var button = new Button();
            grid.Children.Add(button);
            var parent = LogicalTreeHelper.GetParent(button);
            Assert.AreEqual(grid, parent);
            Assert.AreEqual(grid, button.LogicalAncestors().Single());
        }

        [Test, RequiresSTA]
        public void AllAncestorsTest()
        {
            var gridView = new GridView();
            var gridViewColumn = new System.Windows.Controls.GridViewColumn();
            gridView.Columns.Add(gridViewColumn);
            Assert.AreEqual(gridView, gridViewColumn.AllAncestors().First()); 
        }
    }
}
