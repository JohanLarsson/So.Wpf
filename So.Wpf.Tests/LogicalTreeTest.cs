namespace So.Wpf.Tests
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Misc;
    using NUnit.Framework;

    public class DependencyObjectExtTests
    {
        [Test, RequiresSTA]
        public void VisualAncestorsTest()
        {
            var grid = new Grid();
            var button = new Button();
            grid.Children.Add(button);
            var parent = VisualTreeHelper.GetParent(button);
            Assert.AreEqual(grid, parent);
            Assert.AreEqual(grid, button.VisualAncestors().Single());
        }

        [Test, RequiresSTA]
        public void LogicalAncestorsTest()
        {
            var grid = new Grid();
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
            var gridViewColumn = new GridViewColumn();
            gridView.Columns.Add(gridViewColumn);
            Assert.AreEqual(gridView, gridViewColumn.AllAncestors().First()); 
        }
    }
}
