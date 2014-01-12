using So.Wpf.Misc;

namespace So.Wpf.Tests.Prototypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using NUnit.Framework;

    [RequiresSTA]
    public class DpSubscriberTests
    {
        [Test]
        public void DpSubscriberTest()
        {
            var element = new FrameworkElement();
            var class1 = new DpSubscriber(element, UIElement.RenderTransformOriginProperty);
            var objects= new List<object>();
            var points = new List<DependencyPropertyChangedEventArgs>();
            class1.ValueChanged += (sender, point) =>
            {
                objects.Add( sender);
                points.Add(point);
            };
            var np = new Point(0.4, 0.6);
            element.RenderTransformOrigin = np;
            Assert.AreEqual(np, points.Single().NewValue);
            Assert.AreEqual(element, objects.Single());
        }
    }
}
