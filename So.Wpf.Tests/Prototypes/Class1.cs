namespace So.Wpf.Tests.Prototypes
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class Class1<T> : DependencyObject, IDisposable
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(T),
            typeof(Class1<T>),
            new PropertyMetadata(default(T), Notify));

        public Class1(DependencyObject source, DependencyProperty property)
        {
            var binding = new Binding
            {
                Source = source,
                Path = new PropertyPath(property),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(this, ValueProperty, binding);
        }
        public event EventHandler<T> ValueChanged;
        protected virtual void OnValueChanged(T e)
        {
            EventHandler<T> handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void Notify(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Class1<T>)d).OnValueChanged((T) e.NewValue);
        }
        public void Dispose()
        {
            BindingOperations.ClearAllBindings(this);
        }
    }
}
