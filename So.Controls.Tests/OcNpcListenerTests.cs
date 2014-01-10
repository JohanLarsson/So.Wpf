using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using So.Controls.Tests.Annotations;

namespace So.Controls.Tests
{
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
            var listener = OcNpcListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
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
            var listener = OcNpcListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
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
            var listener = OcNpcListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
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
            var listener = OcNpcListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
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

    public class FakeInpc : INotifyPropertyChanged, IEquatable<FakeInpc>
    {
        public int DummyProperty1 { get { return 1; } }
        public int DummyProperty2 { get { return 2; } }

        public void Raise1()
        {
            OnPropertyChanged("DummyProperty1");
        }

        public void Raise2()
        {
            OnPropertyChanged("DummyProperty2");
        }

        public bool HasHandler { get { return PropertyChanged != null; } }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Equals(FakeInpc other)
        {
            return ReferenceEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FakeInpc)obj);
        }

        public override int GetHashCode()
        {
            return 3;
        }

        public static bool operator ==(FakeInpc left, FakeInpc right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FakeInpc left, FakeInpc right)
        {
            return !Equals(left, right);
        }
    }
}