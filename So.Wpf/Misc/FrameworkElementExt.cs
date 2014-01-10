using System.Windows;

namespace So.Wpf.Misc
{
    public static class FrameworkElementExt
    {
        public static void Initialize(this FrameworkElement element)
        {
            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            element.Arrange(new Rect(element.DesiredSize));
            element.UpdateLayout();
        }
    }
}