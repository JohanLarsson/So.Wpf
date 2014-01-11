namespace So.Wpf.Misc
{
    using System;
    using System.Windows;
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

        public override event EventHandler CanExecuteChanged
        {
            add
            {
                CanExecuteChangedEventManager.AddHandler(this, value);
            }
            remove
            {
                CanExecuteChangedEventManager.RemoveHandler(this, value);
            }
        }

        private event EventHandler InternalCanExecuteChanged;
        public virtual void RaiseCanExecuteChanged()
        {
            EventHandler handler = InternalCanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}