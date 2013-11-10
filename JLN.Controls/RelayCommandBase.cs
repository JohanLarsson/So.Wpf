using System;
using System.Windows.Input;

namespace JLN.Controls
{
    public abstract class RelayCommandBase : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _condition;

        protected RelayCommandBase(Action<object> action, Predicate<object> condition)
        {
            _action = action;
            _condition = condition ?? (o => true);
        }

        protected RelayCommandBase(Action<object> action)
        {
            _action = action;
            _condition = (o) => true;
        }

        public bool CanExecute(object parameter)
        {
            return _condition(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public abstract event EventHandler CanExecuteChanged;
    }
}