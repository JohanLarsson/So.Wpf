﻿namespace So.Wpf.Controls
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    public class EditableTextBlock : TextBox
    {
        public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.Register(
            "SelectAllTextOnFocus",
            typeof(bool),
            typeof(EditableTextBlock),
            new PropertyMetadata(true));

        public EditableTextBlock()
        {
            SetTextBlockStyle();
            AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(ToggleTriStateFocus));
            AddHandler(LostFocusEvent, new RoutedEventHandler((sender, args) => SetTextBlockStyle()));
        }
        [Description("Text: string"), Category("Common Properties")]
        public bool SelectAllTextOnFocus
        {
            get { return (bool)GetValue(SelectAllTextOnFocusProperty); }
            set { SetValue(SelectAllTextOnFocusProperty, value); }
        }
        private void SetTextBlockStyle()
        {
            Background = null;
            BorderThickness = new Thickness(0);
            Padding = new Thickness(-2, 0, -2, 0);
            Focusable = false;
            IsReadOnly = true;
        }
        private void SetTextBoxStyle()
        {
            // Background = null;
            BorderThickness = new Thickness(1);
            Padding = new Thickness(-3, -2, -3, -2);
            IsReadOnly = false;
        }
        private void ToggleTriStateFocus(object sender, MouseButtonEventArgs e)
        {
            if (!SelectAllTextOnFocus)
            {
                Focusable = true;
                Focus();
                SetTextBoxStyle();
            }
            if (!Focusable)
            {
                Focusable = true;
                Focus();
                e.Handled = true;
            }
            else if (IsReadOnly)
            {
                SetTextBoxStyle();
                SelectAll();
                e.Handled = true;
            }
        }
    }
}
