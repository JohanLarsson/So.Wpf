using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;

namespace So.Controls.Tests
{
    class GridTests
    {
        [Test, RequiresSTA]
        public void ColumnsTest()
        {
            var grid = new Grid();
            So.Controls.AttachedProperties.Grid.SetRows(grid,"*;*;*");
        }

        [Test]
        public void ParseGridLengthsTest()
        {
            string s = "* *";
            IEnumerable<GridLength> actual  = So.Controls.AttachedProperties.Grid.ToGridLengths(s).ToArray();
            CollectionAssert.AreEqual(new[] { new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star) }, actual);
        }
    }
}
