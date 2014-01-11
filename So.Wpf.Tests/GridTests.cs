using System.Windows.Shapes;

namespace So.Wpf.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using NUnit.Framework;
    [RequiresSTA]
    public class GridTests
    {
        [Test]
        public void RowsTest()
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetRows(grid, "*;*;*");
            //Assert.AreEqual(new GridLength(1, GridUnitType.Star), grid.ColumnDefinitions.Single().Width);
            CollectionAssert.AreEqual(grid.RowDefinitions.Select(x => x.Height), new[] { new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star) });
        }

        [Test]
        public void ColumnsTest()
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetColumns(grid, "*;*;*");
            CollectionAssert.AreEqual(grid.ColumnDefinitions.Select(x => x.Width), new[] { new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star) });
            var rectangle = new Rectangle();
            grid.Children.Add(rectangle);
            Grid.SetColumn(rectangle, 1);
        }

        [Test, RequiresSTA]
        public void CellTest()
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetColumns(grid, "*;*;*");
            AttachedProperties.Grid.SetRows(grid, "*;*;*");
            var rectangle = new Rectangle();
            grid.Children.Add(rectangle);
            AttachedProperties.Grid.SetCell(rectangle, "0 1");
            Assert.AreEqual(0, Grid.GetRow(rectangle));
            Assert.AreEqual(1, Grid.GetColumn(rectangle));
        }

        private static readonly object[] GridLengthCases =
        {
            new object[] {"*", new[] {new GridLength(1, GridUnitType.Star)}},
            new object[] {"* *", new[] {new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star)}},
            new object[] {"*;* *", new[] {new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star)}},
            new object[] {"Auto *", new[] {new GridLength(1, GridUnitType.Auto), new GridLength(1, GridUnitType.Star)}},
            new object[] {"5 4*", new[] {new GridLength(5, GridUnitType.Pixel), new GridLength(4, GridUnitType.Star)}},
        };

        [Test, TestCaseSource("GridLengthCases")]
        public void ParseGridLengthsTest(string s, GridLength[] expected)
        {
            var actual = AttachedProperties.Grid.ToGridLengths(s).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
