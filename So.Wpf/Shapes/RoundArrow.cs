namespace So.Wpf.Shapes
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class RoundArrow : Shape
    {
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
            "Length",
            typeof(double),
            typeof(RoundArrow),
            new PropertyMetadata(10.0, OnGeometryChanged));

        public static readonly DependencyProperty ShaftWidthProperty = DependencyProperty.Register(
            "ShaftWidth",
            typeof(double),
            typeof(RoundArrow),
            new PropertyMetadata(3.0, OnGeometryChanged));

        public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register(
            "HeadWidth",
            typeof(double),
            typeof(RoundArrow),
            new PropertyMetadata(6.0, OnGeometryChanged));

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(
            "StartAngle",
            typeof(double),
            typeof(RoundArrow),
            new PropertyMetadata(default(double), OnGeometryChanged));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(SweepDirection),
            typeof(RoundArrow),
            new PropertyMetadata(default(SweepDirection), OnGeometryChanged));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(
            "Radius",
            typeof(double),
            typeof(RoundArrow),
            new PropertyMetadata(10.0, OnGeometryChanged));

        static RoundArrow()
        {
            StretchProperty.OverrideMetadata(typeof(RoundArrow), (PropertyMetadata)new FrameworkPropertyMetadata((object)Stretch.None));
        }

        [Category("Appearance")]
        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        [Category("Appearance")]
        public double ShaftWidth
        {
            get { return (double)GetValue(ShaftWidthProperty); }
            set { SetValue(ShaftWidthProperty, value); }
        }

        [Category("Appearance")]
        public double HeadWidth
        {
            get { return (double)GetValue(HeadWidthProperty); }
            set { SetValue(HeadWidthProperty, value); }
        }

        [Category("Appearance")]
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        [Category("Appearance")]
        public SweepDirection Direction
        {
            get { return (SweepDirection)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        [Category("Appearance")]
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                // Create a StreamGeometry for describing the shape
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule.EvenOdd;

                double halfHeadWidth = HeadWidth / 2;
                var cp = new Point(this.Radius + halfHeadWidth, this.Radius + halfHeadWidth);
                using (StreamGeometryContext context = geometry.Open())
                {
                    double headStartOffsetAngle = ((Length - halfHeadWidth) / Radius) * 180 / Math.PI * (Direction == SweepDirection.Clockwise ? -1 : 1);
                    var startRot = new RotateTransform(StartAngle, cp.X, cp.Y);
                    var headRot = new RotateTransform(StartAngle + headStartOffsetAngle, cp.X, cp.Y);
                    double halfShaftWidth = ShaftWidth / 2;
                    double xi = cp.X + Radius - halfShaftWidth;
                    double xo = cp.X + Radius + halfShaftWidth;

                    double xhi = cp.X + Radius - halfHeadWidth;
                    double xho = cp.X + Radius + halfHeadWidth;
                    var pi = new Point(xi, cp.Y);
                    var po = new Point(xo, cp.Y);
                    var phi = new Point(xhi, cp.Y);
                    var pho = new Point(xho, cp.Y);
                    var ep = new Point(cp.X + Radius, cp.Y + (halfHeadWidth * (Direction == SweepDirection.Clockwise ? -1 : 1)));

                    var startPoint = startRot.Transform(pi);
                    var p1 = headRot.Transform(pi);
                    var p2 = headRot.Transform(phi);
                    var p3 = headRot.Transform(ep);
                    var p4 = headRot.Transform(pho);
                    var p5 = headRot.Transform(po);
                    var p6 = startRot.Transform(po);

                    context.BeginFigure(startPoint, true, true);
                    context.ArcTo(p1, new Size(Radius - halfShaftWidth, Radius - halfShaftWidth), headStartOffsetAngle, Math.Abs(headStartOffsetAngle) > 180, Direction == SweepDirection.Clockwise ? SweepDirection.Counterclockwise : SweepDirection.Clockwise, true, true);
                    context.LineTo(p2, true, true);
                    context.LineTo(p3, true, true);
                    context.LineTo(p4, true, true);
                    context.LineTo(p5, true, true);
                    context.ArcTo(p6, new Size(Radius + halfShaftWidth, Radius + halfShaftWidth), headStartOffsetAngle, Math.Abs(headStartOffsetAngle) > 180, Direction, true, true);
                    context.Close();
                }

                // Freeze the geometry for performance benefits
                geometry.Freeze();
                return geometry;
            }
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
            double num = 2 * (this.Radius + (HeadWidth / 2));
            return new Size(num, num);
        }
        private static void OnGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs ek)
        {
            RoundArrow roundArrow = (RoundArrow)d;
            roundArrow.InvalidateVisual();
        }
    }
}
