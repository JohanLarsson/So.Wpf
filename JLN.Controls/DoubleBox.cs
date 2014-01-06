﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace JLN.Controls
{
    public class DoubleBox : TextBox
    {
        public DoubleBox()
        {
            this.AddHandler(MouseWheelEvent, new RoutedEventHandler(OnMouseWheel));
        }

        private void OnMouseWheel(object sender, RoutedEventArgs routedEventArgs)
        {
            throw new NotImplementedException();
        }

        internal void Update(bool increase)
        {
            double d;
            if(!double.TryParse(Text,out d))
                return;
            int i =(int) (d);

        }
    }
}
