namespace So.Wpf.Misc
{
    using System;
    using System.Windows.Input;
    public class RelayCommand : RelayCommandBase
    {
        public RelayCommand(Action<object> action, Predicate<object> condition) : base(action, condition)
        {
        }

        public RelayCommand(Action<object> action) : base(action)
        {
        }

        /// <summary>
        /// http://stackoverflow.com/a/2588145/1069200
        /// </summary>
        public override event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}