namespace So.Wpf.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Threading;
    public static class OcExt
    {
        private static readonly object Lock = new object();
        public static void ThreadSafeAdd<T>(this ObservableCollection<T> collection, T newItem, bool disableCollectionSynchronizationWhenDone =false)
        {
            BindingOperations.EnableCollectionSynchronization(collection, Lock);
            collection.Add(newItem);
            if(disableCollectionSynchronizationWhenDone)
                BindingOperations.DisableCollectionSynchronization(collection);
        }
        public static void InvokeAdd<T>(this ObservableCollection<T> collection, T newItem)
        {
            collection.Invoke(() => collection.Add(newItem));
        }
        public static void InvokeAddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems)
        {
            collection.Invoke(() =>
            {
                foreach (var newItem in newItems)
                {
                    collection.Add(newItem);
                }
            });
        }
        public static void InvokeRemove<T>(this ObservableCollection<T> collection, T oldItem)
        {
            collection.Invoke(() => collection.Remove(oldItem));
        }
        public static void InvokeClear<T>(this ObservableCollection<T> collection)
        {
            collection.Invoke(() => collection.Clear());
        }
        public static void Invoke<T>(this ObservableCollection<T> col, Action action)
        {
            Dispatcher dispatcher = Application.Current != null
                ? Application.Current.Dispatcher
                : Dispatcher.CurrentDispatcher;
            dispatcher.Invoke(action);
        }
        public static DispatcherOperation AddAsync<T>(this ObservableCollection<T> collection, T newItem)
        {
            return collection.InvokeAsync(() => collection.Add(newItem));
        }
        public static DispatcherOperation AddRangeAsync<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems)
        {
            return collection.InvokeAsync(() =>
            {
                foreach (var newItem in newItems)
                {
                    collection.Add(newItem);
                }
            });
        }
        public static DispatcherOperation RemoveAsync<T>(this ObservableCollection<T> collection, T oldItem)
        {
            return collection.InvokeAsync(() => collection.Remove(oldItem));
        }
        public static DispatcherOperation ClearAsync<T>(this ObservableCollection<T> collection)
        {
            return collection.InvokeAsync(() => collection.Clear());
        }
        public static DispatcherOperation InvokeAsync<T>(this ObservableCollection<T> col, Action action)
        {
            Dispatcher dispatcher = Application.Current != null
                ? Application.Current.Dispatcher
                : Dispatcher.CurrentDispatcher;
            return dispatcher.InvokeAsync(action);
        }
    }
}
