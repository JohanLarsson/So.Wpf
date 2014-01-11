using System;

namespace So.Wpf.Misc
{
    using System.Windows.Media;
    public static class ColorExt
    {
        /// <summary>
        /// http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
        /// </summary>
        /// <param name="hue">0-1</param>
        /// <param name="saturation">0-1</param>
        /// <param name="luminance">0-1</param>
        /// <returns></returns>
        public static Color ColorFromHsl(double hue, double saturation, double luminance)
        {
            if (hue < 0 || hue > 1)
            {
                throw new ArgumentOutOfRangeException("hue", hue,"Must be in the range 0-1");
            }
            if (saturation < 0 || saturation > 1)
            {
                throw new ArgumentOutOfRangeException("saturation", saturation, "Must be in the range 0-1");
            }
            if (luminance < 0 || luminance > 1)
            {
                throw new ArgumentOutOfRangeException("luminance", luminance, "Must be in the range 0-1");
            }
            double v;
            double r, g, b;

            r = luminance;   // default to gray
            g = luminance;
            b = luminance;
            v = (luminance <= 0.5) 
                ? (luminance * (1.0 + saturation)) 
                : (luminance + saturation - (luminance * saturation));
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = luminance + luminance - v;
                sv = (v - m) / v;
                hue *= 6.0;
                sextant = (int)hue;
                fract = hue - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                    case 6:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }

            var rb = System.Convert.ToByte(r * 255.0f);
            var gb = System.Convert.ToByte(g * 255.0f);
            var bb = System.Convert.ToByte(b * 255.0f);
            return Color.FromArgb(255, rb, gb, bb);
        }
    }
}
