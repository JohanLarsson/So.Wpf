using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using So.Controls.Misc;

namespace So.Controls.Tests
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
}
