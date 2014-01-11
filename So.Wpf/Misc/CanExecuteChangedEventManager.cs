namespace So.Wpf.Misc
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    public class CanExecuteChangedEventManager : WeakEventManager
    {
        static CanExecuteChangedEventManager()
        {
            SetCurrentManager(typeof(CanExecuteChangedEventManager), new CanExecuteChangedEventManager());
        }
        public static CanExecuteChangedEventManager CurrentManager
        {
            get
            {
                return (CanExecuteChangedEventManager)GetCurrentManager(typeof(CanExecuteChangedEventManager));
            }
        }
        public static void AddHandler(ICommand source, EventHandler handler)
        {
            CurrentManager.ProtectedAddHandler(source, handler);
        }
        public static void RemoveHandler(ICommand source, EventHandler handler)
        {
            CurrentManager.ProtectedRemoveHandler(source, handler);
        }
        public static void AddListener(ICommand source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }
        public static void RemoveListener(ICommand source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }
        //protected override ListenerList NewListenerList()
        //{
        //    return new ListenerList();
        //}
        protected override void StartListening(object source)
        {
            ((ICommand)source).CanExecuteChanged += DeliverEvent;
        }
        protected override void StopListening(object source)
        {
            ((ICommand)source).CanExecuteChanged -= DeliverEvent;
        }
    }
}
