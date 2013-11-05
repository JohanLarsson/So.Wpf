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
            var events = new  List<FakeInpc>();
            var listener = OcPropertyChangedListener.Create(oc, (sender, args) => events.Add((FakeInpc) sender));
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise();
            Assert.AreEqual(fakeInpc, events.Single());
        }

        [Test]
        public void AddTwiceRemoveOnceTest()
        {
            var fakeInpc = new FakeInpc();
            var oc = new ObservableCollection<FakeInpc>();
            var events = new List<FakeInpc>();
            var listener = OcPropertyChangedListener.Create(oc, (sender, args) => events.Add((FakeInpc)sender));
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise();
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
            var listener = OcPropertyChangedListener.Create(oc, (sender, args) => events.Add((FakeInpc)sender));
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise();
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
            var listener = OcPropertyChangedListener.Create(oc, (sender, args) => events.Add((FakeInpc)sender));
            Assert.IsFalse(fakeInpc.HasHandler);
            oc.Add(fakeInpc);
            Assert.IsTrue(fakeInpc.HasHandler);
            fakeInpc.Raise();
            Assert.AreEqual(fakeInpc, events.Single());
            oc.Clear();
            Assert.IsFalse(fakeInpc.HasHandler);
        }
    }

    public class FakeInpc : INotifyPropertyChanged
    {
        public string PropertyName { get { return "PropName"; } }
        public void Raise()
        {
            OnPropertyChanged(PropertyName);
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