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
        public void GetPropertyNameTest()
        {
            var propertyName = RefelectionHelper.GetPropertyName(() => Prop);
            Assert.AreEqual("Prop",propertyName);
        }

        public string Prop { get; set; }
    }

    public class FakeWithProperty
    {
        public int Prop { get; set; }
    }
}
