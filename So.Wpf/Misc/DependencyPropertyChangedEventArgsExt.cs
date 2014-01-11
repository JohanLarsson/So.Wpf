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
        public static bool IsAttaching(this DependencyPropertyChangedEventArgs e)
        {
            return e.OldValue == null;
        }
        public static bool IsDetatching(this DependencyPropertyChangedEventArgs e)
        {
            return e.NewValue == null;
        }
    }
}
