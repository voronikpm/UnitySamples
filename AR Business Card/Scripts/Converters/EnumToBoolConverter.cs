#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
#else
using System.Windows.Data;
#endif
using System;
using System.Globalization;

#endregion

namespace Assets.Scripts.Converters
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    }
}