namespace So.Wpf.Tests
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Misc;
    using NUnit.Framework;
    [RequiresSTA]
    public class DependencyPropertyExtTests
    {
        private ListView _listView;
        private GridView _gridView;
        private Grid _grid;
        private Button _button;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _listView = new ListView();
            _gridView = new GridView();
            _listView.View = _gridView;
            _grid = new Grid();
            _button = new Button();
            _grid.Children.Add(_button);
        }

        [Test]
        public void AllAncestorsTest()
        {
            Assert.AreEqual(_listView, _gridView.AllAncestors().Single());
        }

        [Test]
        public void LogicalAncestorsTest()
        {
            Assert.AreEqual(_grid, LogicalTreeHelper.GetParent(_button));
            Assert.AreEqual(_grid, _button.LogicalAncestors().Single());
        }

        [Test]
        public void VisualAncestorsTest()
        {
            Assert.AreEqual(_grid, LogicalTreeHelper.GetParent(_button));
            Assert.AreEqual(_grid, _button.VisualAncestors().Single());
        }
    }
}
