namespace So.Wpf.Tests.Prototypes
{
    using System;
    using System.Linq.Expressions;
    using System.Windows;
    using System.Windows.Data;

    public class ObservableDependencyValue<T> : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof (T),
            typeof (ObservableDependencyValue<T>),
            new PropertyMetadata(default(T), StaticHandleValueChanged));
        public ObservableDependencyValue()
        {
        }
        public ObservableDependencyValue(BindingBase bindingBase)
        {
            Bind(bindingBase);
        }

        public event Action<T> ValueChanged;
        public T Value
        {
            get { return (T)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public ObservableDependencyValue<T> Bind(BindingBase bindingBase)
        {
            BindingOperations.SetBinding(this, ValueProperty, bindingBase);

            return this;
        }
        public ObservableDependencyValue<T> Bind(Expression<Func<T>> expr, BindingMode mode = BindingMode.OneWay)
        {
            var path = RootedPropertyPath.Create(expr);

            return Bind(new Binding(path.Path) { Source = path.Target, Mode = mode });
        }
        public void Notify()
        {
            Action<T> valueChanged = ValueChanged;
            if (valueChanged != null)
            {
                valueChanged(Value);
            }
        }
        private static void StaticHandleValueChanged(DependencyObject self, DependencyPropertyChangedEventArgs args)
        {
            ((ObservableDependencyValue<T>)self).HandleValueChanged((T)args.OldValue, (T)args.NewValue);
        }
        private void HandleValueChanged(T oldValue, T value)
        {
            Notify();
        }
    }

    public static class ObservableDependencyValue
    {
        public static ObservableDependencyValue<T> Create<T>(Expression<Func<T>> expr, BindingMode mode = BindingMode.OneWay)
        {
            return new ObservableDependencyValue<T>().Bind(expr, mode);
        }
    }
}