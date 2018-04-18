#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System;
using System.Reflection;
using Assets.Scripts.Helpers;
using Noesis;

#else
using Assets.Scripts.Helpers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
#endif

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for ColorCanvas.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class ColorCanvas : UserControl
    {
        #region Fields

        #region Static Fields and Constants

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorCanvas), new PropertyMetadata(default(Color)));
        public static readonly DependencyProperty SpectrumSliderValueProperty = DependencyProperty.Register("SpectrumSliderValue", typeof(float), typeof(ColorCanvas), new PropertyMetadata(default(float), SpectrumSlider_OnValueChanged));
        public static readonly DependencyProperty ClosestRalColorProperty = DependencyProperty.Register("ClosestRalColor", typeof(string), typeof(ColorCanvas), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty SelectedHueProperty = DependencyProperty.Register("SelectedHue", typeof(Color), typeof(ColorCanvas), new PropertyMetadata(default(Color)));

        #endregion

        #endregion

        #region Constructors

        public ColorCanvas()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #region Regular Properties

        public string ClosestRalColor
        {
            get { return (string) GetValue(ClosestRalColorProperty); }
            set { SetValue(ClosestRalColorProperty, value); }
        }

        public Color SelectedColor
        {
            get { return (Color) GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public Color SelectedHue
        {
            get { return (Color) GetValue(SelectedHueProperty); }
            set { SetValue(SelectedHueProperty, value); }
        }

        public float SpectrumSliderValue
        {
            get { return (float) GetValue(SpectrumSliderValueProperty); }
            set { SetValue(SpectrumSliderValueProperty, value); }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void CalculateColor(Point p)
        {
            if(_spectrumSlider == null)
                return;
            var hsv = new HsvColor(360 - _spectrumSlider.Value, p.X, 1 - p.Y);
            var currentColor = ColorUtilities.ConvertHsvToRgb(hsv.H, hsv.S, hsv.V);
            var ralColor = ColorUtilities.ClosestRalColor(currentColor);
            ClosestRalColor = ralColor;
            SelectedColor = ralColor;
        }

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/ColorCanvas.xaml");
            _spectrumSlider = FindName("_spectrumSlider") as Slider;
            _shadeCanvas = FindName("_shadeCanvas") as Canvas;
        }
#endif

        private void SelectColor(Point p, bool calculateColor = true)
        {
            if(p.Y < 0)
                p.Y = 0;
            if(p.X < 0)
                p.X = 0;
            if(p.X > _shadeCanvas.ActualWidth)
                p.X = _shadeCanvas.ActualWidth;
            if(p.Y > _shadeCanvas.ActualHeight)
                p.Y = _shadeCanvas.ActualHeight;
            p.X = p.X / _shadeCanvas.ActualWidth;
            p.Y = p.Y / _shadeCanvas.ActualHeight;
            if(calculateColor)
                CalculateColor(p);
        }

        private void SelectHue(float value)
        {
            SelectedHue = ColorUtilities.ConvertHsvToRgb(360 - value, 1, 1);
        }

        private void ShadingCanvas_TouchDown(object sender, TouchEventArgs e)
        {
#if NOESIS
            SelectColor(e.GetTouchPoint(_shadeCanvas));
#endif
        }

        private static void SpectrumSlider_OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var colorCanvas = dependencyObject as ColorCanvas;
            if(colorCanvas != null)
                colorCanvas.SelectHue((float) dependencyPropertyChangedEventArgs.NewValue);
        }

        #endregion

        #region Overriding Methods

#if NOESIS
        protected override void Connect(object source, string eventName, string handlerName)
        {
            var eventInfo = source.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.Instance);
            if(eventInfo == null)
                return;
            var methodInfo = GetType().GetMethod(handlerName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if(methodInfo == null)
                return;
            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
            eventInfo.AddEventHandler(source, handler);
        }
#endif

        #endregion

        #endregion

#if NOESIS
        private Canvas _shadeCanvas;
        private Slider _spectrumSlider;
#endif
    }
}