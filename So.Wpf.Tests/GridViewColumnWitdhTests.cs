namespace So.Wpf.Tests
{
    using System.Linq;
    using System.Windows.Controls;
    using Misc;
    using NUnit.Framework;
    internal class GridViewColumnWitdhTests
    {
        [Test, RequiresSTA]
        public void OneStarSizedTest()
        {
            var view = new GridView();
            var lw = new ListView();
            var gvc = new System.Windows.Controls.GridViewColumn();
            lw.View = view;
            var dependencyObjects = view.LogicalAncestors().ToArray();
        }
    }
}
