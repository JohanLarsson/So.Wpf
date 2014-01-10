namespace So.Wpf.Tests
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using NUnit.Framework;
    using Misc;
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
}
