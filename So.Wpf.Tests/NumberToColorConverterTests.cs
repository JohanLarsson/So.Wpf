using System;

namespace So.Wpf.Tests
{
    using Misc;
    using NUnit.Framework;
    public class ColorExtTests
    {
        [TestCase(0, 0, 0, 255, 0, 0, 0)]
        [TestCase(1, 1, 1, 255, 255, 255, 255)]
        [TestCase(0, 1, 0.5, 255, 255, 0, 0)]
        [TestCase(0.33333333, 1, 0.5, 255, 0, 255, 0)]
        [TestCase(0.66666666, 1, 0.5, 255, 0, 0, 255)]
        [TestCase(1, 1, 0.5, 255, 255, 0, 0)]
        [TestCase(-0.1, 0.5, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(1.1, 0.5, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(-0.1, 0.5, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(1.1, 0.5, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0.5, -0.1, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0.5, 1.1, 0.5, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0.5, 0.5, -0.1, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        [TestCase(0.5, 0.5, 1.1, 0, 0, 0, 0, ExpectedException = typeof(ArgumentOutOfRangeException))]
        public void TestNameTest(double h, double s, double l, byte a, byte r, byte g, byte b)
        {
            var actual = ColorExt.ColorFromHsl(h, s, l);
            Assert.AreEqual(a, actual.A, "Alpha");
            Assert.AreEqual(r, actual.R, "Red");
            Assert.AreEqual(g, actual.G, "Green");
            Assert.AreEqual(b, actual.B, "Blue");
        }
    }
}
