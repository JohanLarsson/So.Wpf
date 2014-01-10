namespace So.Wpf.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using NUnit.Framework;
    public class GridTests
    {
        [Test, RequiresSTA]
        public void ColumnsTest()
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetRows(grid, "*;*;*");
        }

        [Test]
        public void ParseGridLengthsTest()
        {
            string s = "* *";
            IEnumerable<GridLength> actual = AttachedProperties.Grid.ToGridLengths(s).ToArray();
            CollectionAssert.AreEqual(new[] { new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star) }, actual);
        }
    }
}
