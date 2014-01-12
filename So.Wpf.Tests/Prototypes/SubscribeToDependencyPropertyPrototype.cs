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
    public class SubscribeToDependencyPropertyPrototype
    {
        [Test]
        public void TestNameTest()
        {
            var element = new FrameworkElement();
            PropertyMetadata propertyMetadata = UIElement.RenderTransformOriginProperty.GetMetadata(element);
            var binding = new Binding
            {
                Source = element,
                Path = new PropertyPath(UIElement.RenderTransformOriginProperty)
            };
            //BindingOperations.SetBinding()
            //UIElement.RenderTransformOriginProperty.OverrideMetadata(,);
        }
        [Test]
        public void Class1Test()
        {
            var element = new FrameworkElement();
            var class1 = new Class1<Point>(element, UIElement.RenderTransformOriginProperty);
            object o = null;
            Point p = new Point(-1, -1);
            class1.ValueChanged += (sender, point) =>
            {
                o = sender;
                p = point;
            };
            var np = new Point(0.4, 0.6);
            element.RenderTransformOrigin = np;
            Assert.AreEqual(p, np);
        }
        [Test]
        public void ObserveDpTest()
        {
            var element = new FrameworkElement();
            var observableDependencyProperty = ObservableDependencyValue.Create(() => element.RenderTransformOrigin);
            var points = new List<Point>();
            observableDependencyProperty.ValueChanged += points.Add;
            var p = new Point(0.4, 0.6);
            element.RenderTransformOrigin = p;
            Assert.AreEqual(p, points.Single());
        }
        private void Handler(object sender, DataTransferEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
