using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace So.Wpf.Tests.Prototypes
{
    using System.ComponentModel;

    public class WeakReferenceTests
    {
        [Test, Explicit]
        public void IsAliveTest()
        {
            var classWithEvent = new ClassWithEvent();
            var subscriber = new Subscriber(classWithEvent);
            classWithEvent.RaiseEvent();
            Assert.AreEqual(1,subscriber.count);
            var weakReference = new WeakReference(subscriber);
            Assert.IsTrue(weakReference.IsAlive,"Assert is alive before GC");
            subscriber = null;
            GC.Collect();
            Assert.IsFalse(weakReference.IsAlive,"Assert collected");
        }
    }

    public class Subscriber
    {
        public int count = 0;
        public Subscriber(ClassWithEvent classWithEvent)
        {
            classWithEvent.Event += (sender, args) => count++;
        }
    }
    public class ClassWithEvent
    {
        public event EventHandler Event;

        public void RaiseEvent()
        {
            EventHandler handler = Event;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}
