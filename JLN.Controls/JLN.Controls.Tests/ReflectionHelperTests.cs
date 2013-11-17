using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JLN.Controls.Tests
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
