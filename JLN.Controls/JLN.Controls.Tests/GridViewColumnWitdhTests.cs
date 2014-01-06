using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using GridViewColumn = JLN.Controls.AttachedProperties.GridViewColumn;

namespace JLN.Controls.Tests
{
    class GridViewColumnWitdhTests
    {
        [Test, RequiresSTA]
        public void OneStarSizedTest()
        {
            var listView = new ListView();
            listView.Width = 200;
            var gridView = new GridView();
            gridView.Columns.Add(new System.Windows.Controls.GridViewColumn { Width = 50 });
            var gridViewColumn = new System.Windows.Controls.GridViewColumn();
            GridViewColumn.SetColumnWidth(gridViewColumn,new GridLength(2,GridUnitType.Star));
            gridView.Columns.Add(gridViewColumn);
            listView.View = gridView;
            Assert.AreEqual(150,gridViewColumn.ActualWidth);
        }
    }
}
