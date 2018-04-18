#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
#else
using System.Windows.Data;
using System.Windows.Media;
#endif
using System;
using System.Globalization;

#endregion

namespace Assets.Scripts.Converters
{
    public class ColorToBrushConverter : IValueConverter
    {
        #region Interface Implementations

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? new SolidColorBrush((Color) value) : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (SolidColorBrush) value != null ? ((SolidColorBrush) value).Color : (Color?) null;
        }

        #endregion

        #endregion
    }
}