﻿using System;
using System.Collections.Generic;
using System.Windows;

namespace So.Wpf.AttachedProperties
{
    public static class Canvas
    {
        public static readonly DependencyProperty XProperty =
            DependencyProperty.RegisterAttached("X", typeof(double), typeof(Canvas), new PropertyMetadata(default(double), OnXchanged));

        public static readonly DependencyProperty XpositionRelativeToProperty =
            DependencyProperty.RegisterAttached("XpositionRelativeTo", typeof(XpositionedRelativeTo), typeof(Canvas),
                new PropertyMetadata(default(XpositionedRelativeTo), XpositionRelativeToChanged));

        private static readonly HashSet<FrameworkElement> AttachedTo = new HashSet<FrameworkElement>();
        private static void OnXchanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (IsChanged(e))
                UpdateX((FrameworkElement)o);
        }
        public static void SetX(FrameworkElement element, double value)
        {
            element.SetValue(XProperty, value);
        }
        public static double GetX(FrameworkElement element)
        {
            return (double)element.GetValue(XProperty);
        }


        private static void XpositionRelativeToChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (IsChanged(e))
                UpdateX((FrameworkElement)o);
        }
        public static void SetXpositionRelativeTo(FrameworkElement element, XpositionedRelativeTo value)
        {
            element.SetValue(XpositionRelativeToProperty, value);
        }
        public static XpositionedRelativeTo GetXpositionRelativeTo(FrameworkElement element)
        {
            return (XpositionedRelativeTo)element.GetValue(XpositionRelativeToProperty);
        }

        public static readonly DependencyProperty YProperty =
    DependencyProperty.RegisterAttached("Y", typeof(double), typeof(Canvas), new PropertyMetadata(default(double), OnYchanged));
        private static void OnYchanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (IsChanged(e))
                UpdateY((FrameworkElement)o);
        }
        public static void SetY(FrameworkElement element, double value)
        {
            element.SetValue(YProperty, value);
        }
        public static double GetY(FrameworkElement element)
        {
            return (double)element.GetValue(YProperty);
        }

        public static readonly DependencyProperty YpositionRelativeToProperty =
            DependencyProperty.RegisterAttached("YpositionRelativeTo", typeof(YpositionedRelativeTo), typeof(Canvas), new PropertyMetadata(default(YpositionedRelativeTo), YpositionRelativeToChanged));
        private static void YpositionRelativeToChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (IsChanged(e))
                UpdateY((FrameworkElement)o);
        }
        public static void SetYpositionRelativeTo(FrameworkElement element, YpositionedRelativeTo value)
        {
            element.SetValue(YpositionRelativeToProperty, value);
        }
        public static YpositionedRelativeTo GetYpositionRelativeTo(FrameworkElement element)
        {
            return (YpositionedRelativeTo)element.GetValue(YpositionRelativeToProperty);
        }
        private static void OnSizeChanged(object o, SizeChangedEventArgs args)
        {
            if (args.WidthChanged)
                UpdateX((FrameworkElement)o);
            if (args.HeightChanged)
                UpdateY((FrameworkElement)o);
        }
        private static void UpdateX(FrameworkElement fe)
        {
            if (!AttachedTo.Contains(fe))
            {
                fe.AddHandler(FrameworkElement.SizeChangedEvent, new SizeChangedEventHandler(OnSizeChanged));
                AttachedTo.Add(fe);
            }

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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            System.Windows.Controls.Canvas.SetLeft(fe, x);
        }
        private static void UpdateY(FrameworkElement fe)
        {
            if (!AttachedTo.Contains(fe))
            {
                fe.AddHandler(FrameworkElement.SizeChangedEvent, new SizeChangedEventHandler(OnSizeChanged));
                AttachedTo.Add(fe);
            }
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
            System.Windows.Controls.Canvas.SetBottom(fe, y);
        }
        private static bool IsChanged(DependencyPropertyChangedEventArgs e)
        {
            return e.NewValue != e.OldValue;
        }
    }

    public enum XpositionedRelativeTo
    {
        Center,
        LeftEdge,
        RightEdge
    }
    public enum YpositionedRelativeTo
    {
        Center,
        TopEdge,
        BottomEdge
    }
}