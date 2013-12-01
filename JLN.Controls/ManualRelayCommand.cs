﻿using System;
using System.Windows;

namespace JLN.Controls
{
    public class ManualRelayCommand : RelayCommandBase
    {
        public ManualRelayCommand(Action<object> action, Predicate<object> condition)
            : base(action, condition)
        {
        }

        public ManualRelayCommand(Action<object> action)
            : base(action)
        {
        }


        public override event EventHandler CanExecuteChanged;

        public virtual void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                Application.Current.Dispatcher.InvokeAsync(() => handler(this, EventArgs.Empty));
            }
        }
    }
}