namespace So.Wpf.Tests
{
    using System;
    using Misc;
    using NUnit.Framework;
    public class ReflectionHelperTests
    {
        public int FakeProperty { get; set; }

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
        public class FakeClass
        {
            public DateTime Time { get; set; }
        }
    }
}
