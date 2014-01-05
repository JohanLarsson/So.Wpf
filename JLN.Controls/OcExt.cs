using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace JLN.Controls
{
    public static class OcExt
    {
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
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> newItems)
        {
            collection.Invoke(() =>
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
        public static DispatcherOperation InvokeAsync<T>(this ObservableCollection<T> col, Action action)
        {
            Dispatcher dispatcher = Application.Current != null
                ? Application.Current.Dispatcher
                : Dispatcher.CurrentDispatcher;
            return dispatcher.InvokeAsync(action);
        }
        public static void Invoke<T>(this ObservableCollection<T> col, Action action)
        {
            Dispatcher dispatcher = Application.Current != null
                ? Application.Current.Dispatcher
                : Dispatcher.CurrentDispatcher;
            dispatcher.Invoke(action);
        }
    }
}
