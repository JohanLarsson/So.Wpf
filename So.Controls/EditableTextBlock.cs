using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace So.Controls
{
    public class EditableTextBlock : TextBox
    {
        public EditableTextBlock()
        {
            SetTextBlockStyle();
            AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(ToggleTriStateFocus));
            AddHandler(LostFocusEvent, new RoutedEventHandler((sender, args) => SetTextBlockStyle()));
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
            //Background = null;
            BorderThickness = new Thickness(1);
            Padding = new Thickness(-3, -2, -3, -2);
            IsReadOnly = false;
        }

        public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.Register("SelectAllTextOnFocus", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(true));
        [Description("Text: string"), Category("Common Properties")]
        public bool SelectAllTextOnFocus
        {
            get { return (bool)GetValue(SelectAllTextOnFocusProperty); }
            set { SetValue(SelectAllTextOnFocusProperty, value); }
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
