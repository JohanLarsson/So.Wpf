namespace So.Wpf.Misc
{
    using System.Windows;
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