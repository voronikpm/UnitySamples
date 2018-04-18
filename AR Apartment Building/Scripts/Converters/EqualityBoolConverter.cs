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
    public class EqualityBoolConverter : IValueConverter
    {
        #region Interface Implementations

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}