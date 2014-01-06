using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JLN.Controls
{
    public class EditableTextBlock : TextBox
    {
        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBlock), new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
        }

        public EditableTextBlock()
        {
            Background = null;
            BorderThickness = new Thickness(0);
            Padding = new Thickness(-2, 0, -2, 0);
        }

        public static readonly DependencyProperty SelectAllTextOnFocusProperty = DependencyProperty.Register("SelectAllTextOnFocus", typeof(bool), typeof(EditableTextBlock), new PropertyMetadata(true, SelectAllTextChangedCallback));
        [Description("Text: string"), Category("Common Properties")]
        public bool SelectAllTextOnFocus
        {
            get { return (bool)GetValue(SelectAllTextOnFocusProperty); }
            set { SetValue(SelectAllTextOnFocusProperty, value); }
        }
        private static void SelectAllTextChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var etb = (EditableTextBlock)sender;
            if ((bool)args.NewValue)
            {
                etb.AddHandler(PreviewMouseLeftButtonDownEvent, etb._mouseButtonEventHandler, true);
                etb.AddHandler(GotKeyboardFocusEvent, etb._routedEventHandler, true);
                etb.AddHandler(MouseDoubleClickEvent, etb._routedEventHandler, true);
            }
            else
            {
                etb.RemoveHandler(PreviewMouseLeftButtonDownEvent, etb._mouseButtonEventHandler);
                etb.RemoveHandler(GotKeyboardFocusEvent, etb._routedEventHandler);
                etb.RemoveHandler(MouseDoubleClickEvent, etb._routedEventHandler);
            }
        }

        private readonly MouseButtonEventHandler _mouseButtonEventHandler = SelectivelyIgnoreMouseButton;
        /// <summary>
        /// http://stackoverflow.com/questions/660554/how-to-automatically-select-all-text-on-focus-in-wpf-textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
                parent = VisualTreeHelper.GetParent(parent);

            if (parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    // If the text box is not yet focussed, give it the focus and
                    // stop further processing of this click event.
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        private readonly RoutedEventHandler _routedEventHandler = SelectAllText;
        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }
    }
}
