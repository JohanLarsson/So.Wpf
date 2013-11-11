using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using JLN.Controls.Annotations;
using Expression = System.Linq.Expressions.Expression;

namespace JLN.Controls
{
    public class OcPropertyChangedListener<T> : INotifyPropertyChanged where T : INotifyPropertyChanged
    {
        private readonly ObservableCollection<T> _collection;
        private readonly string _propertyName;
        private readonly Dictionary<T, int> _items = new Dictionary<T, int>();

        public OcPropertyChangedListener(ObservableCollection<T> collection, string propertyName = "")
        {
            _collection = collection;
            _propertyName = propertyName ?? "";
            CollectionChangedEventManager.AddHandler(collection, CollectionChanged);
        }

        public OcPropertyChangedListener(ObservableCollection<T> collection, Expression<Func<object>> property):
            this(collection,OcPropertyChangedListener.GetPropertyName(property)) { }


        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Add(e.NewItems.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Remove(e.OldItems.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Add(e.NewItems.Cast<T>());
                    Remove(e.OldItems.Cast<T>());
                    break;
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Reset();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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
                    PropertyChangedEventManager.AddHandler(item, ChildPropertyChanged, _propertyName);
                }
            }
        }

        private void Remove(IEnumerable<T> oldItems)
        {
            foreach (T item in oldItems)
            {
                _items[item]--;
                if (_items[item] == 0)
                {
                    PropertyChangedEventManager.RemoveHandler(item, ChildPropertyChanged, _propertyName);
                }
            }
        }

        private void Reset()
        {
            foreach (T item in _items.Keys.Except(_collection).ToList())
            {
                PropertyChangedEventManager.RemoveHandler(item, ChildPropertyChanged, _propertyName);
                _items.Remove(item);
            }
            Dictionary<T, int> dictionary = _collection.ToDictionary(x => x, x => _collection.Count(y => y.Equals(x)));
            foreach (T newItem in dictionary.Keys.Except(_items.Keys))
            {
                PropertyChangedEventManager.AddHandler(newItem, ChildPropertyChanged, _propertyName);
                _items.Add(newItem, dictionary[newItem]);
            }
            foreach (var i in dictionary)
            {
                _items[i.Key] = i.Value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void ChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(sender, new PropertyChangedEventArgs(e.PropertyName));
        }
    }

    public static class OcPropertyChangedListener
    {
        public static OcPropertyChangedListener<T> Create<T>(ObservableCollection<T> collection, string propertyName = "") where T : INotifyPropertyChanged
        {
            return new OcPropertyChangedListener<T>(collection, propertyName);
        }

        public static OcPropertyChangedListener<T> Create<T>(ObservableCollection<T> collection, Expression<Func<object>> property) where T : INotifyPropertyChanged
        {
            return new OcPropertyChangedListener<T>(collection, GetPropertyName(property));
        }

        public static string GetPropertyName(Expression<Func<object>> property)
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;
            var unaryExpression = property.Body as UnaryExpression;
            var expression = unaryExpression.Operand;
            throw new NotImplementedException("message");
            
        }
    }
}