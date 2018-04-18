#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
#endif
using System.Reflection;

#endregion

namespace Assets.Scripts.Helpers
{
    internal struct HsvColor
    {
        #region Fields

        #region  Public Fields

        public double H;
        public double S;
        public double V;

        #endregion

        #endregion

        #region Constructors

        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }

        #endregion
    }

    internal struct RalColor
    {
        #region Fields

        #region  Public Fields

        public Color Color;
        public string Id;

        #endregion

        #endregion

        #region Constructors

        public RalColor(string id, Color color)
        {
            Id = id;
            Color = color;
        }

        public RalColor(string id, string color) : this(id, ColorUtilities.HexToColor(color))
        {
        }

        #endregion

        #region Methods

        #region Operators

        public static implicit operator Color(RalColor color)
        {
            return color.Color;
        }

        public static implicit operator string(RalColor color)
        {
            return color.Id;
        }

        #endregion

        #endregion
    }

    internal static class ColorUtilities
    {
        #region Fields

        #region Static Fields and Constants

        public static readonly Dictionary<string, Color> KnownColors = GetKnownColors();

        public static readonly List<RalColor> RalColors = new List<RalColor>
                                                          {
                                                              new RalColor("RAL1000", "#BEBD7F"),
                                                              new RalColor("RAL1001", "#C2B078"),
                                                              new RalColor("RAL1002", "#C6A664"),
                                                              new RalColor("RAL1003", "#E5BE01"),
                                                              new RalColor("RAL1004", "#CDA434"),
                                                              new RalColor("RAL1005", "#A98307"),
                                                              new RalColor("RAL1006", "#E4A010"),
                                                              new RalColor("RAL1007", "#DC9D00"),
                                                              new RalColor("RAL1011", "#8A6642"),
                                                              new RalColor("RAL1012", "#C7B446"),
                                                              new RalColor("RAL1013", "#EAE6CA"),
                                                              new RalColor("RAL1014", "#E1CC4F"),
                                                              new RalColor("RAL1015", "#E6D690"),
                                                              new RalColor("RAL1016", "#EDFF21"),
                                                              new RalColor("RAL1017", "#F5D033"),
                                                              new RalColor("RAL1018", "#F8F32B"),
                                                              new RalColor("RAL1019", "#9E9764"),
                                                              new RalColor("RAL1020", "#999950"),
                                                              new RalColor("RAL1021", "#F3DA0B"),
                                                              new RalColor("RAL1023", "#FAD201"),
                                                              new RalColor("RAL1024", "#AEA04B"),
                                                              new RalColor("RAL1026", "#FFFF00"),
                                                              new RalColor("RAL1027", "#9D9101"),
                                                              new RalColor("RAL1028", "#F4A900"),
                                                              new RalColor("RAL1032", "#D6AE01"),
                                                              new RalColor("RAL1033", "#F3A505"),
                                                              new RalColor("RAL1034", "#EFA94A"),
                                                              new RalColor("RAL1035", "#6A5D4D"),
                                                              new RalColor("RAL1036", "#705335"),
                                                              new RalColor("RAL1037", "#F39F18"),
                                                              new RalColor("RAL2000", "#ED760E"),
                                                              new RalColor("RAL2001", "#C93C20"),
                                                              new RalColor("RAL2002", "#CB2821"),
                                                              new RalColor("RAL2003", "#FF7514"),
                                                              new RalColor("RAL2004", "#F44611"),
                                                              new RalColor("RAL2005", "#FF2301"),
                                                              new RalColor("RAL2007", "#FFA420"),
                                                              new RalColor("RAL2008", "#F75E25"),
                                                              new RalColor("RAL2009", "#F54021"),
                                                              new RalColor("RAL2010", "#D84B20"),
                                                              new RalColor("RAL2011", "#EC7C26"),
                                                              new RalColor("RAL2012", "#E55137"),
                                                              new RalColor("RAL2013", "#C35831"),
                                                              new RalColor("RAL3000", "#AF2B1E"),
                                                              new RalColor("RAL3001", "#A52019"),
                                                              new RalColor("RAL3002", "#A2231D"),
                                                              new RalColor("RAL3003", "#9B111E"),
                                                              new RalColor("RAL3004", "#75151E"),
                                                              new RalColor("RAL3005", "#5E2129"),
                                                              new RalColor("RAL3007", "#412227"),
                                                              new RalColor("RAL3009", "#642424"),
                                                              new RalColor("RAL3011", "#781F19"),
                                                              new RalColor("RAL3012", "#C1876B"),
                                                              new RalColor("RAL3013", "#A12312"),
                                                              new RalColor("RAL3014", "#D36E70"),
                                                              new RalColor("RAL3015", "#EA899A"),
                                                              new RalColor("RAL3016", "#B32821"),
                                                              new RalColor("RAL3017", "#E63244"),
                                                              new RalColor("RAL3018", "#D53032"),
                                                              new RalColor("RAL3020", "#CC0605"),
                                                              new RalColor("RAL3022", "#D95030"),
                                                              new RalColor("RAL3024", "#F80000"),
                                                              new RalColor("RAL3026", "#FE0000"),
                                                              new RalColor("RAL3027", "#C51D34"),
                                                              new RalColor("RAL3028", "#CB3234"),
                                                              new RalColor("RAL3031", "#B32428"),
                                                              new RalColor("RAL3032", "#721422"),
                                                              new RalColor("RAL3033", "#B44C43"),
                                                              new RalColor("RAL4001", "#6D3F5B"),
                                                              new RalColor("RAL4002", "#922B3E"),
                                                              new RalColor("RAL4003", "#DE4C8A"),
                                                              new RalColor("RAL4004", "#641C34"),
                                                              new RalColor("RAL4005", "#6C4675"),
                                                              new RalColor("RAL4006", "#A03472"),
                                                              new RalColor("RAL4007", "#4A192C"),
                                                              new RalColor("RAL4008", "#924E7D"),
                                                              new RalColor("RAL4009", "#A18594"),
                                                              new RalColor("RAL4010", "#CF3476"),
                                                              new RalColor("RAL4011", "#8673A1"),
                                                              new RalColor("RAL4012", "#6C6874"),
                                                              new RalColor("RAL5000", "#354D73"),
                                                              new RalColor("RAL5001", "#1F3438"),
                                                              new RalColor("RAL5002", "#20214F"),
                                                              new RalColor("RAL5003", "#1D1E33"),
                                                              new RalColor("RAL5004", "#18171C"),
                                                              new RalColor("RAL5005", "#1E2460"),
                                                              new RalColor("RAL5007", "#3E5F8A"),
                                                              new RalColor("RAL5008", "#26252D"),
                                                              new RalColor("RAL5009", "#025669"),
                                                              new RalColor("RAL5010", "#0E294B"),
                                                              new RalColor("RAL5011", "#231A24"),
                                                              new RalColor("RAL5012", "#3B83BD"),
                                                              new RalColor("RAL5013", "#1E213D"),
                                                              new RalColor("RAL5014", "#606E8C"),
                                                              new RalColor("RAL5015", "#2271B3"),
                                                              new RalColor("RAL5017", "#063971"),
                                                              new RalColor("RAL5018", "#3F888F"),
                                                              new RalColor("RAL5019", "#1B5583"),
                                                              new RalColor("RAL5020", "#1D334A"),
                                                              new RalColor("RAL5021", "#256D7B"),
                                                              new RalColor("RAL5022", "#252850"),
                                                              new RalColor("RAL5023", "#49678D"),
                                                              new RalColor("RAL5024", "#5D9B9B"),
                                                              new RalColor("RAL5025", "#2A6478"),
                                                              new RalColor("RAL5026", "#102C54"),
                                                              new RalColor("RAL6000", "#316650"),
                                                              new RalColor("RAL6001", "#287233"),
                                                              new RalColor("RAL6002", "#2D572C"),
                                                              new RalColor("RAL6003", "#424632"),
                                                              new RalColor("RAL6004", "#1F3A3D"),
                                                              new RalColor("RAL6005", "#2F4538"),
                                                              new RalColor("RAL6006", "#3E3B32"),
                                                              new RalColor("RAL6007", "#343B29"),
                                                              new RalColor("RAL6008", "#39352A"),
                                                              new RalColor("RAL6009", "#31372B"),
                                                              new RalColor("RAL6010", "#35682D"),
                                                              new RalColor("RAL6011", "#587246"),
                                                              new RalColor("RAL6012", "#343E40"),
                                                              new RalColor("RAL6013", "#6C7156"),
                                                              new RalColor("RAL6014", "#47402E"),
                                                              new RalColor("RAL6015", "#3B3C36"),
                                                              new RalColor("RAL6016", "#1E5945"),
                                                              new RalColor("RAL6017", "#4C9141"),
                                                              new RalColor("RAL6018", "#57A639"),
                                                              new RalColor("RAL6019", "#BDECB6"),
                                                              new RalColor("RAL6020", "#2E3A23"),
                                                              new RalColor("RAL6021", "#89AC76"),
                                                              new RalColor("RAL6022", "#25221B"),
                                                              new RalColor("RAL6024", "#308446"),
                                                              new RalColor("RAL6025", "#3D642D"),
                                                              new RalColor("RAL6026", "#015D52"),
                                                              new RalColor("RAL6027", "#84C3BE"),
                                                              new RalColor("RAL6028", "#2C5545"),
                                                              new RalColor("RAL6029", "#20603D"),
                                                              new RalColor("RAL6032", "#317F43"),
                                                              new RalColor("RAL6033", "#497E76"),
                                                              new RalColor("RAL6034", "#7FB5B5"),
                                                              new RalColor("RAL6035", "#1C542D"),
                                                              new RalColor("RAL6036", "#193737"),
                                                              new RalColor("RAL6037", "#008F39"),
                                                              new RalColor("RAL6038", "#00BB2D"),
                                                              new RalColor("RAL7000", "#78858B"),
                                                              new RalColor("RAL7001", "#8A9597"),
                                                              new RalColor("RAL7002", "#7E7B52"),
                                                              new RalColor("RAL7003", "#6C7059"),
                                                              new RalColor("RAL7004", "#969992"),
                                                              new RalColor("RAL7005", "#646B63"),
                                                              new RalColor("RAL7006", "#6D6552"),
                                                              new RalColor("RAL7008", "#6A5F31"),
                                                              new RalColor("RAL7009", "#4D5645"),
                                                              new RalColor("RAL7010", "#4C514A"),
                                                              new RalColor("RAL7011", "#434B4D"),
                                                              new RalColor("RAL7012", "#4E5754"),
                                                              new RalColor("RAL7013", "#464531"),
                                                              new RalColor("RAL7015", "#434750"),
                                                              new RalColor("RAL7016", "#293133"),
                                                              new RalColor("RAL7021", "#23282B"),
                                                              new RalColor("RAL7022", "#332F2C"),
                                                              new RalColor("RAL7023", "#686C5E"),
                                                              new RalColor("RAL7024", "#474A51"),
                                                              new RalColor("RAL7026", "#2F353B"),
                                                              new RalColor("RAL7030", "#8B8C7A"),
                                                              new RalColor("RAL7031", "#474B4E"),
                                                              new RalColor("RAL7032", "#B8B799"),
                                                              new RalColor("RAL7033", "#7D8471"),
                                                              new RalColor("RAL7034", "#8F8B66"),
                                                              new RalColor("RAL7035", "#D7D7D7"),
                                                              new RalColor("RAL7036", "#7F7679"),
                                                              new RalColor("RAL7037", "#7D7F7D"),
                                                              new RalColor("RAL7038", "#B5B8B1"),
                                                              new RalColor("RAL7039", "#6C6960"),
                                                              new RalColor("RAL7040", "#9DA1AA"),
                                                              new RalColor("RAL7042", "#8D948D"),
                                                              new RalColor("RAL7043", "#4E5452"),
                                                              new RalColor("RAL7044", "#CAC4B0"),
                                                              new RalColor("RAL7045", "#909090"),
                                                              new RalColor("RAL7046", "#82898F"),
                                                              new RalColor("RAL7047", "#D0D0D0"),
                                                              new RalColor("RAL7048", "#898176"),
                                                              new RalColor("RAL8000", "#826C34"),
                                                              new RalColor("RAL8001", "#955F20"),
                                                              new RalColor("RAL8002", "#6C3B2A"),
                                                              new RalColor("RAL8003", "#734222"),
                                                              new RalColor("RAL8004", "#8E402A"),
                                                              new RalColor("RAL8007", "#59351F"),
                                                              new RalColor("RAL8008", "#6F4F28"),
                                                              new RalColor("RAL8011", "#5B3A29"),
                                                              new RalColor("RAL8012", "#592321"),
                                                              new RalColor("RAL8014", "#382C1E"),
                                                              new RalColor("RAL8015", "#633A34"),
                                                              new RalColor("RAL8016", "#4C2F27"),
                                                              new RalColor("RAL8017", "#45322E"),
                                                              new RalColor("RAL8019", "#403A3A"),
                                                              new RalColor("RAL8022", "#212121"),
                                                              new RalColor("RAL8023", "#A65E2E"),
                                                              new RalColor("RAL8024", "#79553D"),
                                                              new RalColor("RAL8025", "#755C48"),
                                                              new RalColor("RAL8028", "#4E3B31"),
                                                              new RalColor("RAL8029", "#763C28"),
                                                              new RalColor("RAL9001", "#FDF4E3"),
                                                              new RalColor("RAL9002", "#E7EBDA"),
                                                              new RalColor("RAL9003", "#F4F4F4"),
                                                              new RalColor("RAL9004", "#282828"),
                                                              new RalColor("RAL9005", "#0A0A0A"),
                                                              new RalColor("RAL9006", "#A5A5A5"),
                                                              new RalColor("RAL9007", "#8F8F8F"),
                                                              new RalColor("RAL9010", "#FFFFFF"),
                                                              new RalColor("RAL9011", "#1C1C1C"),
                                                              new RalColor("RAL9016", "#F6F6F6"),
                                                              new RalColor("RAL9017", "#1E1E1E"),
                                                              new RalColor("RAL9018", "#D7D7D7"),
                                                              new RalColor("RAL9022", "#9C9C9C"),
                                                              new RalColor("RAL9023", "#828282")
                                                          };

        private static readonly IEnumerable<Color> RgbColors = RalColors.Select(x => x.Color);

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public static RalColor ClosestRalColor(Color color)
        {
            return RalColors.First(x => x == RgbColors.ElementAt(ClosestColor(RgbColors, color)));
        }

        public static Color ConvertHsvToRgb(double h, double s, double v)
        {
            double r, g, b;
            if(Math.Abs(s) < double.Epsilon)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                if(Math.Abs(h - 360) < double.Epsilon)
                    h = 0;
                else
                    h = h / 60;
                var i = (int) Math.Truncate(h);
                double f = h - i;
                double p = v * (1.0 - s);
                double q = v * (1.0 - s * f);
                double t = v * (1.0 - s * (1.0 - f));
                switch(i)
                {
                    case 0:
                    {
                        r = v;
                        g = t;
                        b = p;
                        break;
                    }
                    case 1:
                    {
                        r = q;
                        g = v;
                        b = p;
                        break;
                    }
                    case 2:
                    {
                        r = p;
                        g = v;
                        b = t;
                        break;
                    }
                    case 3:
                    {
                        r = p;
                        g = q;
                        b = v;
                        break;
                    }
                    case 4:
                    {
                        r = t;
                        g = p;
                        b = v;
                        break;
                    }
                    default:
                    {
                        r = v;
                        g = p;
                        b = q;
                        break;
                    }
                }
            }
#if NOESIS
            var retVal = new Color((byte) Math.Round(r * 255), (byte) Math.Round(g * 255), (byte) Math.Round(b * 255), 255);
#else
            var retVal = Color.FromArgb(255, (byte)(Math.Round(r * 255)), (byte)(Math.Round(g * 255)), (byte)(Math.Round(b * 255)));
#endif
            return retVal;
        }

        public static HsvColor ConvertRgbToHsv(int r, int b, int g)
        {
            double h = 0;
            double min = Math.Min(Math.Min(r, g), b);
            double v = Math.Max(Math.Max(r, g), b);
            double delta = v - min;
            double s = Math.Abs(v) < double.Epsilon ? 0 : delta / v;
            if(Math.Abs(s) < double.Epsilon)
            {
                h = 0;
            }
            else
            {
                if(Math.Abs(r - v) < double.Epsilon)
                    h = (g - b) / delta;
                else if(Math.Abs(g - v) < double.Epsilon)
                    h = 2 + (b - r) / delta;
                else if(Math.Abs(b - v) < double.Epsilon)
                    h = 4 + (r - g) / delta;
                h *= 60;
                if(h < 0.0)
                    h = h + 360;
            }
            return new HsvColor
                   {
                       H = h,
                       S = s,
                       V = v / 255
                   };
        }

        public static string FormatColorString(string stringToFormat, bool isUsingAlphaChannel)
        {
            if(!isUsingAlphaChannel && stringToFormat.Length == 9)
                return stringToFormat.Remove(1, 2);
            return stringToFormat;
        }

        public static List<Color> GenerateHsvSpectrum()
        {
            var colorsList = new List<Color>(8);
            for(var i = 0; i < 29; i++)
                colorsList.Add(ConvertHsvToRgb(i * 12, 1, 1));
            colorsList.Add(ConvertHsvToRgb(0, 1, 1));
            return colorsList;
        }

        public static string GetColorName(this Color color)
        {
            string colorName = KnownColors.Where(kvp => kvp.Value.Equals(color)).Select(kvp => kvp.Key).FirstOrDefault();
            if(string.IsNullOrEmpty(colorName))
                colorName = color.ToString();
            return colorName;
        }

        public static Color HexToColor(string hex)
        {
            byte r = byte.Parse(hex.Substring(1, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(3, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(5, 2), NumberStyles.HexNumber);
#if NOESIS
            return new Color(r, g, b);
#else
            return new Color();
#endif
        }

        public static HsvColor ToHsvColor(this Color color)
        {
#if NOESIS
            return ConvertRgbToHsv(color.Ri, color.Bi, color.Gi);
#else
            return ConvertRgbToHsv(color.R, color.B, color.G);
#endif
        }

        public static Color ToRgbColor(this HsvColor color)
        {
            return ConvertHsvToRgb(color.H, color.S, color.V);
        }
#if NOESIS
        public static UnityEngine.Color ToUnityColor(this Color color)
        {
            return new UnityEngine.Color(color.R, color.G, color.B, color.A);
        }
#endif

        private static int ClosestColor(IEnumerable<Color> colors, Color target)
        {
            double hue1 = target.ToHsvColor().H;
            double num1 = ColorNum(target);
            //var diffs = colors.Select(n => Math.Abs(ColorNum(n) - num1) + GetHueDistance(n.ToHsvColor().H, hue1)).ToList();
            var diffs = colors.Select(x => ColorDiff(x, target)).ToList();
            //var diffs = colors.Select(x => GetHsvDistance(x.ToHsvColor(), target.ToHsvColor())).ToList();
            double diffMin = diffs.Min(x => x);
            return diffs.FindIndex(n => Math.Abs(n - diffMin) < double.Epsilon);
        }

        private static double ColorDiff(Color c1, Color c2)
        {
            return Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R) + (c1.G - c2.G) * (c1.G - c2.G) + (c1.B - c2.B) * (c1.B - c2.B));
        }

        private static double ColorNum(Color c, double factorSat = 100, double factorBri = 100)
        {
            return c.ToHsvColor().S * factorSat + GetBrightness(c) * factorBri;
        }

        private static float GetBrightness(Color c)
        {
            return (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
        }

        private static double GetHsvDistance(HsvColor color1, HsvColor color2)
        {
            double dh = Math.Min(Math.Abs(color1.H - color2.H), 360 - Math.Abs(color1.H - color2.H)) / 10;
            double ds = color1.S - color2.S;
            double dv = (color1.V - color2.V) * 2.55;
            return Math.Sqrt(dh * dh + ds * ds + dv * dv);
        }

        private static double GetHueDistance(double hue1, double hue2)
        {
            double d = Math.Abs(hue1 - hue2);
            return d > 180 ? 360 - d : d;
        }

        private static Dictionary<string, Color> GetKnownColors()
        {
            var colorProperties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            return colorProperties.ToDictionary(p => p.Name, p => (Color) p.GetValue(null, null));
        }

        #endregion

        #endregion
    }
}