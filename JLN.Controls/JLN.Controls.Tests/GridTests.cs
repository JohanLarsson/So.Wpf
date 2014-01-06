using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    class GridTests
    {
        [Test, RequiresSTA]
        public void ColumnsTest()
        {
            var grid = new Grid();
            JLN.Controls.AttachedProperties.Grid.SetRows(grid,"*;*;*");
        }

        [Test]
        public void ParseGridLengthsTest()
        {
            string s = "* *";
            IEnumerable<GridLength> actual  = JLN.Controls.AttachedProperties.Grid.ToGridLengths(s).ToArray();
            CollectionAssert.AreEqual(new[] { new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star) }, actual);
        }
    }
}
