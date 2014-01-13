namespace So.Wpf.Misc
{
    using System;
    using System.Windows;
    public class SizeChangedEventManager : WeakEventManager
    {
        private static SizeChangedEventManager _currentManager;
        static SizeChangedEventManager()
        {
            SetCurrentManager(typeof(SizeChangedEventManager), new SizeChangedEventManager());
        }
        private SizeChangedEventManager()
        {
        }
        public static SizeChangedEventManager CurrentManager
        {
            get
            {
                return _currentManager ??
                       (_currentManager = (SizeChangedEventManager)GetCurrentManager(typeof(SizeChangedEventManager)));
            }
        }
        public static void AddHandler(System.Windows.FrameworkElement source, SizeChangedEventHandler handler)
        {
            CurrentManager.ProtectedAddHandler(source, handler);
        }
        public static void RemoveHandler(System.Windows.FrameworkElement source, SizeChangedEventHandler handler)
        {
            CurrentManager.ProtectedRemoveHandler(source, handler);
        }
        public static void AddListener(System.Windows.FrameworkElement source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }
        public static void RemoveListener(System.Windows.FrameworkElement source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }
        protected override void StartListening(object source)
        {
            ((System.Windows.FrameworkElement)source).SizeChanged += DeliverEvent;
        }
        protected override void StopListening(object source)
        {
            ((System.Windows.FrameworkElement)source).SizeChanged -= DeliverEvent;
        }
    }
}