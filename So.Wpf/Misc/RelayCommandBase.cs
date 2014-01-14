namespace So.Wpf.Misc
{
    using System;
    using System.Windows.Input;
    public abstract class RelayCommandBase : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _condition;

        protected RelayCommandBase(Action<object> action, Predicate<object> condition)
            :this(action)
        {
            _condition = condition ?? (o => true);
        }

        protected RelayCommandBase(Action<object> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");
            _action = action;
            _condition = (o) => true;
        }

        public abstract event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return _condition(parameter);
        }
        public void Execute(object parameter)
        {
            _action(parameter);
        }
    }
}