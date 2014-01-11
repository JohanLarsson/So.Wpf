using System;
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

        [TestCase("* *", "* *", "0 0", 0, 0)]
        [TestCase("* *", "* *", "1 1", 1, 1)]
        [TestCase("*;*;*", "*;*;*", "0 0", 0, 0)]
        [TestCase("*;*;*", "*;*;*", "0 1", 0, 1)]
        [TestCase("*;*;*", "*;*;*", "0 2", 0, 2)]
        [TestCase("*;*;*", "*;*;*", "0 3", 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase("*;*;*", "*;*;*", "0 0", 0, 0)]
        [TestCase("*;*;*", "*;*;*", "1 0", 1, 0)]
        [TestCase("*;*;*", "*;*;*", "2 0", 2, 0)]
        [TestCase("*;*;*", "*;*;*", "3 0", 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void CellTest(string rows, string columns, string cell, int row, int col)
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetColumns(grid, rows);
            AttachedProperties.Grid.SetRows(grid, columns);
            var rectangle = new Rectangle();
            grid.Children.Add(rectangle);
            AttachedProperties.Grid.SetCell(rectangle, cell);
            Assert.AreEqual(row, Grid.GetRow(rectangle));
            Assert.AreEqual(col, Grid.GetColumn(rectangle));
        }

        [Test]
        public void TwoCellsTest()
        {
            var grid = new Grid();
            AttachedProperties.Grid.SetColumns(grid, "* *");
            AttachedProperties.Grid.SetRows(grid, "* *");
            var r1 = new Rectangle();
            var r2 = new Rectangle();
            grid.Children.Add(r1);
            grid.Children.Add(r2);
            AttachedProperties.Grid.SetCell(r1, "0 0");
            AttachedProperties.Grid.SetCell(r2, "1 1");
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
