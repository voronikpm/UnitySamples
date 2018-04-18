#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System.Windows;
#endif

namespace Assets.Scripts.UI
{
    public class AnimationUtilities : DependencyObject
    {
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(AnimationUtilities), new PropertyMetadata(default(bool)));

        public static void SetIsVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static bool GetIsVisible(DependencyObject element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }
    }
}