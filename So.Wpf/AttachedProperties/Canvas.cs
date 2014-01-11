namespace So.Wpf.AttachedProperties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using Misc;

    public static class Canvas
    {
        public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached(
            "X",
            typeof(double),
            typeof(Canvas),
            new PropertyMetadata(default(double), UpdateX));

        public static readonly DependencyProperty XpositionRelativeToProperty = DependencyProperty.RegisterAttached(
            "XpositionRelativeTo",
            typeof(XpositionedRelativeTo),
            typeof(Canvas),
            new PropertyMetadata(XpositionedRelativeTo.LeftEdge, XpositionRelativeToChanged));

        public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached(
            "Y",
            typeof(double),
            typeof(Canvas),
            new PropertyMetadata(default(double), UpdateY));

        public static readonly DependencyProperty YpositionRelativeToProperty = DependencyProperty.RegisterAttached(
            "YpositionRelativeTo",
            typeof(YpositionedRelativeTo),
            typeof(Canvas),
            new PropertyMetadata(YpositionedRelativeTo.BottomEdge, YpositionRelativeToChanged));

        private static readonly HashSet<FrameworkElement> AttachedTo = new HashSet<FrameworkElement>();
        public static void SetX(FrameworkElement element, double value)
        {
            element.SetValue(XProperty, value);
        }
        public static double GetX(FrameworkElement element)
        {
            return (double)element.GetValue(XProperty);
        }
        public static void SetXpositionRelativeTo(FrameworkElement element, XpositionedRelativeTo value)
        {
            element.SetValue(XpositionRelativeToProperty, value);
        }
        public static XpositionedRelativeTo GetXpositionRelativeTo(FrameworkElement element)
        {
            return (XpositionedRelativeTo)element.GetValue(XpositionRelativeToProperty);
        }
        public static void SetY(FrameworkElement element, double value)
        {
            element.SetValue(YProperty, value);
        }
        public static double GetY(FrameworkElement element)
        {
            return (double)element.GetValue(YProperty);
        }
        public static void SetYpositionRelativeTo(FrameworkElement element, YpositionedRelativeTo value)
        {
            element.SetValue(YpositionRelativeToProperty, value);
        }
        public static YpositionedRelativeTo GetYpositionRelativeTo(FrameworkElement element)
        {
            return (YpositionedRelativeTo)element.GetValue(YpositionRelativeToProperty);
        }
        private static void XpositionRelativeToChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.IsAttachingValueEqualTo(XpositionedRelativeTo.RenderTransformOrigin))
            {
                o.AddCallBack(UIElement.RenderTransformOriginProperty, UpdateX);
            }
            if (e.IsDetatchingValueEqualTo(XpositionedRelativeTo.RenderTransformOrigin))
            {
                o.RemoveCallBack(UIElement.RenderTransformOriginProperty, UpdateX);
            }
            UpdateX(o, e);
        }
        private static void YpositionRelativeToChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.IsAttachingValueEqualTo(YpositionedRelativeTo.RenderTransformOrigin))
            {
                o.AddCallBack(UIElement.RenderTransformOriginProperty, UpdateY);
            }
            if (e.IsDetatchingValueEqualTo(YpositionedRelativeTo.RenderTransformOrigin))
            {
                o.RemoveCallBack(UIElement.RenderTransformOriginProperty, UpdateY);
            }
            UpdateY(o, e);
        }
        private static void OnSizeChanged(object o, SizeChangedEventArgs args)
        {
            if (args.WidthChanged)
            {
                UpdateX((FrameworkElement)o);
            }
            if (args.HeightChanged)
            {
                UpdateY((FrameworkElement)o);
            }
        }
        private static void UpdateX(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (!e.IsChanged())
                return;
            var fe = o as FrameworkElement;
            if (fe == null)
                return;
            if (!AttachedTo.Contains(fe))
            {
                fe.AddHandler(FrameworkElement.SizeChangedEvent, new SizeChangedEventHandler(OnSizeChanged));
                AttachedTo.Add(fe);
            }
            UpdateX(fe);
        }
        private static void UpdateX(FrameworkElement fe)
        {
            double x = GetX(fe);
            XpositionedRelativeTo relativeTo = GetXpositionRelativeTo(fe);
            switch (relativeTo)
            {
                case XpositionedRelativeTo.Center:
                    x -= fe.ActualWidth / 2;
                    break;
                case XpositionedRelativeTo.LeftEdge:
                    break;
                case XpositionedRelativeTo.RightEdge:
                    x -= fe.ActualWidth;
                    break;
                case XpositionedRelativeTo.RenderTransformOrigin:
                    x -= fe.RenderTransformOrigin.X * fe.ActualWidth;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            System.Windows.Controls.Canvas.SetLeft(fe, x);
        }
        private static void UpdateY(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (!e.IsChanged())
                return;
            var fe = o as FrameworkElement;
            if (fe == null)
                return;
            if (!AttachedTo.Contains(fe))
            {
                fe.AddHandler(FrameworkElement.SizeChangedEvent, new SizeChangedEventHandler(OnSizeChanged));
                AttachedTo.Add(fe);
            }
            UpdateY(fe);
        }
        private static void UpdateY(FrameworkElement fe)
        {
            double y = GetY(fe);
            YpositionedRelativeTo relativeTo = GetYpositionRelativeTo(fe);
            switch (relativeTo)
            {
                case YpositionedRelativeTo.Center:
                    y -= fe.ActualHeight / 2;
                    break;
                case YpositionedRelativeTo.BottomEdge:
                    break;
                case YpositionedRelativeTo.TopEdge:
                    y -= fe.ActualHeight;
                    break;
                case YpositionedRelativeTo.RenderTransformOrigin:
                    y -= fe.RenderTransformOrigin.Y * fe.ActualHeight;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            System.Windows.Controls.Canvas.SetBottom(fe, y);
        }
    }
}
