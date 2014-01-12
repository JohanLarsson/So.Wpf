namespace So.Wpf.Shapes
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using So.Wpf.Misc;
    public class CrossHair : Shape
    {
        public CrossHair()
        {
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
        }
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X",
            typeof(double),
            typeof(CrossHair),
            new PropertyMetadata(default(double), Xchanged));

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y",
            typeof(double),
            typeof(CrossHair),
            new PropertyMetadata(default(double), Ychanged));

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                return new CombinedGeometry(Xline.RenderedGeometry, Yline.RenderedGeometry);
            }
        }

        private Panel Panel
        {
            get
            {
                return this.VisualAncestors().OfType<Panel>().First();
            }
        }
        private Line Xline
        {
            get
            {
                double actualHeight = Panel.ActualHeight;
                double y = actualHeight - Y;
                return new Line
                {
                    X1 = 0,
                    X2 = Panel.ActualWidth,
                    Y1 = y,
                    Y2 = y
                };
            }
        }
        private Line Yline
        {
            get
            {
                return new Line
                {
                    X1 = X,
                    X2 = X,
                    Y1 = 0,
                    Y2 = Panel.ActualHeight
                };
            }
        }

        private static void Xchanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((CrossHair)o).InvalidateVisual();
        }
        private static void Ychanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((CrossHair)o).InvalidateVisual();
        }
    }
}
