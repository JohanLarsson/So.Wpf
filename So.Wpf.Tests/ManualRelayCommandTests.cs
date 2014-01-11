using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using NUnit.Framework;
using So.Wpf.Misc;

namespace So.Wpf.Tests
{
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
