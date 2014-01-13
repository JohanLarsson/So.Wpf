namespace So.Wpf.Effects
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using Misc;

    /// <summary>An effect that turns the input into shades of a single color.</summary>
    public class FadeToMonochrome : ShaderEffect
    {
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty(
            "Input",
            typeof(FadeToMonochrome),
            0);

        public static readonly DependencyProperty StrengthProperty = DependencyProperty.Register(
            "Strength",
            typeof(double),
            typeof(FadeToMonochrome),
            new UIPropertyMetadata(((double)(0D)), PixelShaderConstantCallback(0)));

        public FadeToMonochrome()
        {
            PixelShader pixelShader = new PixelShader();
            pixelShader.UriSource = PackUriHelper.MakePackUri("Effects/FadeToMonochrome.ps");
            this.PixelShader = pixelShader;

            this.UpdateShaderValue(InputProperty);
            this.UpdateShaderValue(StrengthProperty);
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
        /// <summary>The strength of the effect.</summary>
        public double Strength
        {
            get
            {
                return ((double)(this.GetValue(StrengthProperty)));
            }
            set
            {
                this.SetValue(StrengthProperty, value);
            }
        }
    }
}
