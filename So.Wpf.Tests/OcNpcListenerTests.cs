namespace So.Wpf.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using Misc;
    using NUnit.Framework;
    public class OcNpcListenerTests
    {
        [Test]
        public void SimpleAddTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
        }

        [Test]
        public void SimpleAddPropertyChangedEventManagerTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            PropertyChangedEventManager.AddHandler(listener, (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child), "");
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
        }

        [Test, Explicit]
        public void SimpleAddListenWithPropertyChangedEventManagerTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<ListenerAndChild<FakeInpc>>();
            var listener = OcNpcListener.Create(oc);
            PropertyChangedEventManager.AddHandler(listener, (sender, args) => events.Add((ListenerAndChild<FakeInpc>)sender), "");
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single().Child);
        }

        [Test]
        public void SimpleAddNamedTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc, "DummyProperty1");
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise2();
            Assert.IsFalse(events.Any());
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
        }

        [Test]
        public void AddTwiceRemoveOnceTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
            oc.RemoveAt(1);
            Assert.IsTrue(fakeInpc.HasHandler);
        }

        [Test]
        public void RemoveTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
            oc.Remove(fakeInpc);
            Assert.IsFalse(fakeInpc.HasHandler);
        }

        [Test]
        public void ClearTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
            oc.Clear();
            Assert.IsFalse(fakeInpc.HasHandler);
        }

        [Test]
        public void ReplaceTest()
        {
            var fakeInpc1 = new FakeInpc();
            var fakeInpc2 = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcNpcListener.Create(oc);
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)((ChildPropertyChangedEventArgs)args).Child);
            Assert.IsFalse(fakeInpc1.HasHandler);
            oc.Add(fakeInpc1);
            Assert.IsTrue(fakeInpc1.HasHandler);
            fakeInpc1.Raise1();
            Assert.AreEqual(fakeInpc1, events.Single());
            oc[0] = fakeInpc2;
            Assert.IsFalse(fakeInpc1.HasHandler);
            Assert.IsTrue(fakeInpc2.HasHandler);
        }
    }
}