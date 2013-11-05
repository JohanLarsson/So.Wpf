using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace JLN.Controls
{
    public class OcPropertyChangedListener<T> : OcPropertyChangedListener where T : INotifyPropertyChanged
    {
        private readonly PropertyChangedEventHandler _handler;
        private readonly Dictionary<T, int> _items = new Dictionary<T, int>(); 
        public OcPropertyChangedListener(ObservableCollection<T> collection, PropertyChangedEventHandler handler)
        {
            _handler = handler;
            collection.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Move)
                    return;
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    foreach (T item in _items.Keys.Except(collection).ToList())
                    {
                        item.PropertyChanged -= handler;
                        _items.Remove(item);
                    }
                    Dictionary<T, int> dictionary = collection.ToDictionary(x => x, x => collection.Count(y => y.Equals(x)));
                    foreach (T newItem in dictionary.Keys.Except(_items.Keys))
                    {
                        newItem.PropertyChanged += handler;
                        _items.Add(newItem,dictionary[newItem]);
                    }
                    foreach (var i in dictionary)
                    {
                        _items[i.Key] = i.Value;
                    }
                }
                if (e.OldItems != null)
                    foreach (T item in e.OldItems)
                    {
                        _items[item]--;
                        if (_items[item] == 0)
                        {
                            item.PropertyChanged -= _handler;
                        }
                    }
                if (e.NewItems != null)
                    Add(e.NewItems.Cast<T>());
            };
        }

        private void Add(IEnumerable<T> newItems)
        {
            foreach (T item in newItems)
            {
                if (_items.ContainsKey(item))
                {
                    _items[item]++;
                }
                else
                {
                    _items.Add(item, 1);
                    item.PropertyChanged += _handler;
                }
            }
        }
    }

    public class OcPropertyChangedListener
    {
        public static OcPropertyChangedListener<T> Create<T>(ObservableCollection<T> collection,
    PropertyChangedEventHandler handler) where T : INotifyPropertyChanged
        {
            return new OcPropertyChangedListener<T>(collection, handler);
        }
    }
}