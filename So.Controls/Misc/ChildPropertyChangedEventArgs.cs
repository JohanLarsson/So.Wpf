using System.ComponentModel;

namespace So.Controls
{
    public class ChildPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        public ChildPropertyChangedEventArgs(object child, string propertyName)
            : base(propertyName)
        {
            Child = child;
        }

        public ChildPropertyChangedEventArgs(object sender, PropertyChangedEventArgs e)
            : base(e.PropertyName)
        {
            Child = sender;
        }

        //public ChildPropertyChangedEventArgs(EventPattern<PropertyChangedEventArgs> e)
        //    : base(e.EventArgs.PropertyName)
        //{
        //    Child = e.Sender;
        //}

        public object Child { get; private set; }
    }
}