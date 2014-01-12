using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace So.Wpf.Misc
{
    using System.Windows;

    public static class DependencyPropertyChangedEventArgsExt
    {
        public static bool IsChanged(this DependencyPropertyChangedEventArgs e)
        {
            return !Equals(e.NewValue, e.OldValue);
        }
        internal static bool IsAttaching(this DependencyPropertyChangedEventArgs e)
        {
            return e.OldValue == null && e.NewValue != null;
        }
        internal static bool IsAttachingValueEqualTo<T>(this DependencyPropertyChangedEventArgs e, T value)
        {
            if (!e.IsChanged())
            {
                return false;
            }
            if (!(e.NewValue is T))
            {
                return false;
            }
            return Equals((T)e.NewValue, value);
        }
        internal static bool IsDetatching(this DependencyPropertyChangedEventArgs e)
        {
            return e.NewValue == null && e.OldValue != null;
        }
        internal static bool IsDetatchingValueEqualTo<T>(this DependencyPropertyChangedEventArgs e, T value)
        {
            if (!e.IsChanged())
            {
                return false;
            }
            if (!(e.OldValue is T))
            {
                return false;
            }
            return Equals((T)e.OldValue, value);
        }
    }
}
