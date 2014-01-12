using System;
using System.Windows.Input;
using NUnit.Framework;
using So.Wpf.Misc;

namespace So.Wpf.Tests
{
    [RequiresSTA, Explicit("Can't test it whith dispatcher invoke")]
    public class ManualRelayCommandTests
    {
        /// <summary>
        /// http://agsmith.wordpress.com/2008/04/07/propertydescriptor-addvaluechanged-alternative/
        /// </summary>
        [Test]
        public void CheckWeakTest()
        {
            var command = new ManualRelayCommand(o => { });
            var subscriber = new CommandSubscriber(command);
            WeakReference wr = new WeakReference(subscriber);
            Assert.IsTrue(wr.IsAlive);
            command.RaiseCanExecuteChanged();
            Assert.AreEqual(1, subscriber.Count);
            subscriber = null;
            GC.Collect();
            Assert.IsFalse(wr.IsAlive);
        }

        [Test]
        public void TwoCommandsTest()
        {
            var command1 = new ManualRelayCommand(o => { });
            var subscriber1 = new CommandSubscriber(command1);
            var command2 = new ManualRelayCommand(o => { });
            var subscriber2 = new CommandSubscriber(command2);
            command1.RaiseCanExecuteChanged();
            command2.RaiseCanExecuteChanged();
            Assert.AreEqual(1, subscriber1.Count);
            Assert.AreEqual(1, subscriber2.Count);
        }
    }

    public class CommandSubscriber
    {
        public int Count = 0;
        public CommandSubscriber(ICommand command)
        {
            command.CanExecuteChanged += (sender, args) => Count++;
        }
    }
}
