﻿namespace So.Wpf.Misc
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    public static class DependencyObjectExt
    {
        private static readonly PropertyInfo InheritanceContextProp = typeof(DependencyObject).GetProperty(
            "InheritanceContext",
            BindingFlags.NonPublic | BindingFlags.Instance);

        public static IEnumerable<DependencyObject> LogicalAncestors(this DependencyObject dependencyObject)
        {
            while ((dependencyObject = LogicalTreeHelper.GetParent(dependencyObject)) != null)
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
        /// <summary>
        /// Uses reflection and internal InheritanceContext, potentially fragile
        /// http://stackoverflow.com/a/20988314/1069200
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public static IEnumerable<DependencyObject> AllAncestors(this DependencyObject child)
        {
            while (child != null)
            {
                var parent = LogicalTreeHelper.GetParent(child);
                if (parent == null)
                {
                    if (child is FrameworkElement)
                    {
                        parent = VisualTreeHelper.GetParent(child);
                    }
                    if (parent == null && child is ContentElement)
                    {
                        parent = ContentOperations.GetParent((ContentElement)child);
                    }
                    if (parent == null)
                    {
                        parent = InheritanceContextProp.GetValue(child, null) as DependencyObject;
                    }
                }
                if (parent == null)
                {
                    yield break;
                }
                child = parent;
                yield return parent;
            }
        }
        public static void AddCallBack<T>(this T source, DependencyProperty property, PropertyChangedCallback callback) where T : DependencyObject
        {
            throw new NotImplementedException("Got to figure out how to do this in a weak way");
            ////PropertyMetadata propertyMetadata = property.GetMetadata(source.GetType());
            ////propertyMetadata.PropertyChangedCallback += callback;
            ////UIElement.RenderTransformOriginProperty.OverrideMetadata(source.GetType(),);
        }
        public static void RemoveCallBack<T>(this T source, DependencyProperty property, PropertyChangedCallback callback) where T : DependencyObject
        {
            throw new NotImplementedException("Got to figure out how to do this in a weak way");
            ////PropertyMetadata propertyMetadata = property.GetMetadata(source.GetType());
            ////propertyMetadata.PropertyChangedCallback -= callback;
            ////UIElement.RenderTransformOriginProperty.OverrideMetadata(source.GetType(), new PropertyMetadata(callback));
        }
        public static DpSubscriber Subscribe(this DependencyObject source, DependencyProperty property)
        {
            return new DpSubscriber(source, property);
        }
    }
}
