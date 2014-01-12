namespace So.Wpf.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using Misc;
    using NUnit.Framework;

    [RequiresSTA]
    public class DpSubscriberTests
    {
        [Test]
        public void DpSubscriberTest()
        {
            var element = new FrameworkElement();
            var subscriber = new DpSubscriber(element, UIElement.RenderTransformOriginProperty);
            var objects= new List<object>();
            var points = new List<DependencyPropertyChangedEventArgs>();
            subscriber.ValueChanged += (sender, point) =>
            {
                objects.Add( sender);
                points.Add(point);
            };
            var np = new Point(0.4, 0.6);
            element.RenderTransformOrigin = np;
            Assert.AreEqual(np, points.Single().NewValue);
            Assert.AreEqual(element, objects.Single());
            var weakReference = new WeakReference(subscriber);
            Assert.IsTrue(weakReference.IsAlive, "Assert is alive before GC");
            subscriber = null;
            GC.Collect();
            Assert.IsFalse(weakReference.IsAlive, "Assert collected");
        }
    }
}
