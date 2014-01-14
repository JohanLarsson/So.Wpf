﻿namespace So.Wpf.Misc
{
    using System;
    using System.Windows;
    public class ManualRelayCommand : RelayCommandBase
    {
        private readonly bool _raiseCanExecuteOnDispatcher;
        public ManualRelayCommand(Action<object> action, Predicate<object> condition, bool raiseCanExecuteOnDispatcher = true)
            : base(action, condition)
        {
            _raiseCanExecuteOnDispatcher = raiseCanExecuteOnDispatcher;
        }

        public ManualRelayCommand(Action<object> action, bool raiseCanExecuteOnDispatcher = true)
            : base(action)
        {
            _raiseCanExecuteOnDispatcher = raiseCanExecuteOnDispatcher;
        }

        public override event EventHandler CanExecuteChanged
        {
            add
            {
                InternalCanExecuteChangedEventManager.AddHandler(this, value);
            }
            remove
            {
                InternalCanExecuteChangedEventManager.RemoveHandler(this, value);
            }
        }
        private event EventHandler InternalCanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = InternalCanExecuteChanged;
            if (handler != null)
            {
                if (_raiseCanExecuteOnDispatcher)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => handler(this, new EventArgs())));
                }
                else
                {
                    handler(this, new EventArgs());
                }
            }
        }
        private class InternalCanExecuteChangedEventManager : WeakEventManager
        {
            private static readonly InternalCanExecuteChangedEventManager Manager = new InternalCanExecuteChangedEventManager();
            static InternalCanExecuteChangedEventManager()
            {
                SetCurrentManager(typeof(InternalCanExecuteChangedEventManager), Manager);
            }
            internal static void AddHandler(ManualRelayCommand source, EventHandler handler)
            {
                Manager.ProtectedAddHandler(source, handler);
            }
            internal static void RemoveHandler(ManualRelayCommand source, EventHandler handler)
            {
                Manager.ProtectedRemoveHandler(source, handler);
            }
            ////protected override ListenerList NewListenerList()
            ////{
            ////    return new ListenerList();
            ////}
            protected override void StartListening(object source)
            {
                ((ManualRelayCommand)source).InternalCanExecuteChanged += DeliverEvent;
            }
            protected override void StopListening(object source)
            {
                ((ManualRelayCommand)source).InternalCanExecuteChanged -= DeliverEvent;
            }
        }
    }
}