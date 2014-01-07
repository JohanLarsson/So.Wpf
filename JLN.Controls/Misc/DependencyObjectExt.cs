using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace JLN.Controls.Misc
{
    public static class DependencyObjectExt
    {
        public static IEnumerable<DependencyObject> LogicalAncestors(this DependencyObject dependencyObject)
        {
            while ((dependencyObject = LogicalTreeHelper.GetParent(dependencyObject))!=null)
            {
                yield return dependencyObject;
            }
        }

        public static IEnumerable<DependencyObject> VisualAncestors(this DependencyObject dependencyObject)
        {
            while ((dependencyObject = VisualTreeHelper.GetParent(dependencyObject)) != null)
            {
                yield return dependencyObject;
            }
        }
    }
}
