namespace So.Wpf.Effects 
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;

    public class AngularGradientEffect : ShaderEffect {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(
            "Input", 
            typeof(AngularGradientEffect),
            0);

        public static readonly DependencyProperty CenterPointProperty = DependencyProperty.Register(
            "CenterPoint",
            typeof(Point),
            typeof(AngularGradientEffect), 
            new UIPropertyMetadata(new Point(0.5D, 0.5D), PixelShaderConstantCallback(0)));

        public static readonly DependencyProperty PrimaryColorProperty = DependencyProperty.Register(
            "PrimaryColor", 
            typeof(Color), 
            typeof(AngularGradientEffect), 
            new UIPropertyMetadata(Color.FromArgb(255, 0, 0, 255), PixelShaderConstantCallback(1)));

        public static readonly DependencyProperty SecondaryColorProperty = DependencyProperty.Register(
            "SecondaryColor", 
            typeof(Color),
            typeof(AngularGradientEffect), 
            new UIPropertyMetadata(Color.FromArgb(255, 255, 0, 0), PixelShaderConstantCallback(2)));

        public AngularGradientEffect() {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = new Uri("/So.Wpf;component/Effects/AngularGradientEffect.ps", UriKind.Relative);
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(CenterPointProperty);
            this.UpdateShaderValue(PrimaryColorProperty);
            this.UpdateShaderValue(SecondaryColorProperty);
        }
        public Brush Input {
            get {
                return ((Brush)(this.GetValue(InputProperty)));
            }
            set {
                this.SetValue(InputProperty, value);
            }
        }
        /// <summary>The center of the gradient. </summary>
        public Point CenterPoint {
            get {
                return ((Point)(this.GetValue(CenterPointProperty)));
            }
            set {
                this.SetValue(CenterPointProperty, value);
            }
        }
        /// <summary>The primary color of the gradient. </summary>
        public Color PrimaryColor {
            get {
                return ((Color)(this.GetValue(PrimaryColorProperty)));
            }
            set {
                this.SetValue(PrimaryColorProperty, value);
            }
        }
        /// <summary>The primary color of the gradient. </summary>
        public Color SecondaryColor {
            get {
                return ((Color)(this.GetValue(SecondaryColorProperty)));
            }
            set {
                this.SetValue(SecondaryColorProperty, value);
            }
        }
    }
}
