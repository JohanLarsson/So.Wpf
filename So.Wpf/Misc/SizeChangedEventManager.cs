﻿namespace So.Wpf.Misc
 {
     using System.Windows;
     public class SizeChangedEventManager : WeakEventManager
     {
         private static SizeChangedEventManager _currentManager;
         static SizeChangedEventManager()
         {
             SetCurrentManager(typeof(SizeChangedEventManager), new SizeChangedEventManager());
         }
         private SizeChangedEventManager()
         {
         }
         public static SizeChangedEventManager CurrentManager
         {
             get
             {
                 return _currentManager ??
                        (_currentManager = (SizeChangedEventManager)GetCurrentManager(typeof(SizeChangedEventManager)));
             }
         }
         public static void AddHandler(FrameworkElement source, SizeChangedEventHandler handler)
         {
             CurrentManager.ProtectedAddHandler(source, handler);
         }
         public static void RemoveHandler(FrameworkElement source, SizeChangedEventHandler handler)
         {
             CurrentManager.ProtectedRemoveHandler(source, handler);
         }
         public static void AddListener(FrameworkElement source, IWeakEventListener listener)
         {
             CurrentManager.ProtectedAddListener(source, listener);
         }
         public static void RemoveListener(FrameworkElement source, IWeakEventListener listener)
         {
             CurrentManager.ProtectedRemoveListener(source, listener);
         }
         protected override void StartListening(object source)
         {
             ((FrameworkElement)source).SizeChanged += DeliverEvent;
         }
         protected override void StopListening(object source)
         {
             ((FrameworkElement)source).SizeChanged -= DeliverEvent;
         }
     }
 }
