using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace So.Wpf.Tests.Prototypes
{
    class EffectsTests
    {
        [Test]
        public void TestNameTest()
        {
            Uri partUri = PackUriHelper.Create(new Uri("Effects/AngularGradientEffect.ps", UriKind.Relative));
            Console.WriteLine(partUri);
        }
    }
}
