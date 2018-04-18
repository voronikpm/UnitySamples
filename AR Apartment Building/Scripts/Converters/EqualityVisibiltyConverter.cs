#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System;
using Noesis;
#else
using System.Windows;
using System.Windows.Data;
#endif
using System;
using System.Globalization;

#endregion

namespace Assets.Scripts.Converters
{
    // ReSharper disable once UnusedMember.Global
    public class EqualityVisibiltyConverter : IValueConverter
    {
        #region Interface Implementations

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter.Equals(value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}