using System;
using NUnit.Framework;
using So.Wpf.Misc;

namespace So.Wpf.Tests
{
    class ReflectionHelperTests
    {
        [Test]
        public void Level0PropertyTest()
        {
            string propertyName = ReflectionHelper.GetPropertyName(() => FakeProperty);
            Assert.AreEqual("FakeProperty", propertyName);
        }

        [Test]
        public void Level1PropertyTest()
        {
            FakeClass fake = null;
            string propertyName = ReflectionHelper.GetPropertyName(() => fake.Time);
            Assert.AreEqual("Time", propertyName);
        }

        [Test]
        public void Level2PropertyTest()
        {
            FakeClass fake = null;
            string propertyName = ReflectionHelper.GetPropertyName(() => fake.Time.Date);
            Assert.AreEqual("Date", propertyName);
        }

        class FakeClass
        {
            public DateTime Time { get; set; }
        }
        public int FakeProperty { get; set; }
    }
}
