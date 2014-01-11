namespace So.Wpf.Tests
{
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using NUnit.Framework;
    [RequiresSTA]
    public class CanvasTests
    {
        [Test]
        public void PositionTest()
        {
            var canvas = new Canvas { Width = 200, Height = 200 };
            var ellipse = new Ellipse { Height = 100, Width = 100 };
            canvas.Children.Add(ellipse);
            AttachedProperties.Canvas.SetX(ellipse, 20);
            Assert.AreEqual(20, Canvas.GetLeft(ellipse));
        }
    }
}
