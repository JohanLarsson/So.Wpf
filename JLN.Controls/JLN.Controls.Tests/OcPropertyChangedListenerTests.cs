using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JLN.Controls.Tests.Annotations;
using NUnit.Framework;

namespace JLN.Controls.Tests
{
    public class OcPropertyChangedListenerTests
    {
        [Test]
        public void SimpleAddTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcPropertyChangedListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise1();
            Assert.AreEqual(fakeInpc, events.Single());
        }

        [Test]
        public void SimpleAddNamedTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcPropertyChangedListener.Create(oc, "DummyProperty1");
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
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
            var listener = OcPropertyChangedListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
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
            var listener = OcPropertyChangedListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
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
            var listener = OcPropertyChangedListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
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
            var listener = OcPropertyChangedListener.Create(oc);//, (sender, args) => events.Add((FakeInpc)sender));
            listener.PropertyChanged += (sender, args) => events.Add((FakeInpc)sender);
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

    public class FakeInpc : INotifyPropertyChanged
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
    }
}