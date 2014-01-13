using System.IO.Packaging;

namespace So.Wpf.Effects
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class AngularGradientEffect : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(AngularGradientEffect), 0);
        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register("CenterPoint", typeof(Point), typeof(AngularGradientEffect), new UIPropertyMetadata(new Point(0.5D, 0.5D), PixelShaderConstantCallback(0)));
        public static readonly DependencyProperty StartColorProperty = DependencyProperty.Register("StartColor", typeof(Color), typeof(AngularGradientEffect), new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 255), PixelShaderConstantCallback(1)));
        public static readonly DependencyProperty EndColorProperty = DependencyProperty.Register("EndColor", typeof(Color), typeof(AngularGradientEffect), new UIPropertyMetadata(Color.FromArgb(255, 255, 0, 0), PixelShaderConstantCallback(2)));
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(AngularGradientEffect), new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(3)));
        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register("EndAngle", typeof(double), typeof(AngularGradientEffect), new UIPropertyMetadata(((double)(360D)), PixelShaderConstantCallback(4)));
        public AngularGradientEffect()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = PackUriHelper.CreatePartUri(new Uri("Effects/AngularGradientEffect.ps"));

            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(CenterPointProperty);
            this.UpdateShaderValue(StartColorProperty);
            this.UpdateShaderValue(EndColorProperty);
            this.UpdateShaderValue(StartAngleProperty);
            this.UpdateShaderValue(EndAngleProperty);
        }
        public Brush Input
        {
            get
            {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set
            {
                this.SetValue(InputProperty, value);
            }
        }
        /// <summary>The center of the gradient. </summary>
        public Point CenterPoint
        {
            get
            {
                return ((Point)(this.GetValue(CenterPointProperty)));
            }
            set
            {
                this.SetValue(CenterPointProperty, value);
            }
        }
        /// <summary>The primary color of the gradient. </summary>
        public Color StartColor
        {
            get
            {
                return ((Color)(this.GetValue(StartColorProperty)));
            }
            set
            {
                this.SetValue(StartColorProperty, value);
            }
        }
        /// <summary>The primary color of the gradient. </summary>
        public Color EndColor
        {
            get
            {
                return ((Color)(this.GetValue(EndColorProperty)));
            }
            set
            {
                this.SetValue(EndColorProperty, value);
            }
        }
        /// <summary>The starting angle of the gradient, counterclockwise from X-axis</summary>
        public double StartAngle
        {
            get
            {
                return ((double)(this.GetValue(StartAngleProperty)));
            }
            set
            {
                this.SetValue(StartAngleProperty, value);
            }
        }
        /// <summary>The end angle of the gradient, counterclockwise from X-axis</summary>
        public double EndAngle
        {
            get
            {
                return ((double)(this.GetValue(EndAngleProperty)));
            }
            set
            {
                this.SetValue(EndAngleProperty, value);
            }
        }
    }
}
