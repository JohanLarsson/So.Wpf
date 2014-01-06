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
        private static void SelectAllTextChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var editableTextBlock = (EditableTextBlock)sender;
            if ((bool)args.NewValue)
            {
                editableTextBlock.AddHandler(PreviewMouseLeftButtonDownEvent, editableTextBlock._mouseButtonEventHandler, true);
                editableTextBlock.AddHandler(GotKeyboardFocusEvent, editableTextBlock._routedEventHandler, true);
                editableTextBlock.AddHandler(MouseDoubleClickEvent, editableTextBlock._routedEventHandler, true);
            }
            else
            {
                editableTextBlock.RemoveHandler(PreviewMouseLeftButtonDownEvent, editableTextBlock._mouseButtonEventHandler);
                editableTextBlock.RemoveHandler(GotKeyboardFocusEvent, editableTextBlock._routedEventHandler);
                editableTextBlock.RemoveHandler(MouseDoubleClickEvent, editableTextBlock._routedEventHandler);
            }
        }

        [Description("Text: string"), Category("Common Properties")]
        public bool SelectAllTextOnFocus
        {
            get { return (bool)GetValue(SelectAllTextOnFocusProperty); }
            set { SetValue(SelectAllTextOnFocusProperty, value); }
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
