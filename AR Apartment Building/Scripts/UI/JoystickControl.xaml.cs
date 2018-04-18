#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System;
using System.Reflection;
using Noesis;
using UnityEngine;
using GUI = Noesis.GUI;

#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
#endif

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for JoystickControl.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class JoystickControl : UserControl
    {
        #region Fields

        #region Static Fields and Constants

        public static readonly DependencyProperty JoystickDiameterProperty = DependencyProperty.Register("JoystickDiameter", typeof(float), typeof(JoystickControl), new PropertyMetadata(default(float), JoystickDiameterChanged));
        public static readonly DependencyProperty JoystickXProperty = DependencyProperty.Register("JoystickX", typeof(float), typeof(JoystickControl), new PropertyMetadata(default(float)));
        public static readonly DependencyProperty JoystickYProperty = DependencyProperty.Register("JoystickY", typeof(float), typeof(JoystickControl), new PropertyMetadata(default(float)));
        public static readonly DependencyProperty KnobDiameterProperty = DependencyProperty.Register("KnobDiameter", typeof(float), typeof(JoystickControl), new PropertyMetadata(default(float), KnobDiameterChanged));
        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register("IconPath", typeof(Geometry), typeof(JoystickControl), new PropertyMetadata(default(Geometry)));
        public static readonly DependencyProperty KnobPositionProperty = DependencyProperty.Register("KnobPosition", typeof(Point), typeof(JoystickControl), new PropertyMetadata(default(Point)));

        #endregion

        #region  Private Fields

        private readonly Point _knobAnchor = new Point(0, 0);
#if NOESIS
        private Ellipse _joystick;
#endif

        #endregion

        #endregion

        #region Constructors

        public JoystickControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties

        #region Regular Properties

        public Geometry IconPath
        {
            get { return (Geometry) GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        public float InnerJoystickDiameter
        {
            get { return JoystickDiameter - 40; }
        }

        public float InnerJoystickRadius
        {
            get { return InnerJoystickDiameter / 2; }
        }

        public Point JoystickCenter
        {
            get { return new Point(JoystickRadius, JoystickRadius); }
        }

        public float JoystickDiameter
        {
            get { return (float) GetValue(JoystickDiameterProperty); }
            set { SetValue(JoystickDiameterProperty, value); }
        }

        public float JoystickRadius
        {
            get { return JoystickDiameter / 2; }
        }

        public float JoystickX
        {
            get { return (float) GetValue(JoystickXProperty); }
            set { SetValue(JoystickXProperty, value); }
        }

        public float JoystickY
        {
            get { return (float) GetValue(JoystickYProperty); }
            set { SetValue(JoystickYProperty, value); }
        }

        public float KnobDiameter
        {
            get { return (float) GetValue(KnobDiameterProperty); }
            set { SetValue(KnobDiameterProperty, value); }
        }

        public Point KnobPosition
        {
            get { return (Point) GetValue(KnobPositionProperty); }
            set { SetValue(KnobPositionProperty, value); }
        }

        public float KnobRadius
        {
            get { return KnobDiameter / 2; }
        }

        public float Offset
        {
            get { return JoystickRadius - KnobRadius; }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/JoystickControl.xaml");
            _joystick = FindName("_joystick") as Ellipse;
        }
#endif

        private static void InitKnobPosition(DependencyObject dependencyObject)
        {
            var joystick = dependencyObject as JoystickControl;
            if(joystick != null)
            {
                float center = (joystick.JoystickDiameter - joystick.KnobDiameter) / 2;
                joystick.KnobPosition = new Point(center, center);
            }
        }

        private void Joystick_Down(object sender, TouchEventArgs e)
        {
#if NOESIS
            //TODO make more abstract
            //SceneControllerApartment.Instance.IsTouchingUI = true;
#endif
            //SceneControllerApartment.Instance.RegisterTouch(GetHashCode());
            Joystick_Move(sender, e);
            e.Handled = true;
        }

        private void Joystick_Leave(object sender, TouchEventArgs e)
        {
            UpdateKnobPosition(_knobAnchor);
            e.Handled = true;
        }

        private void Joystick_Move(object sender, TouchEventArgs e)
        {
#if NOESIS
            var joystickPosition = e.GetTouchPoint(_joystick) - JoystickCenter;
            joystickPosition /= JoystickRadius;
            if(Point.Length(joystickPosition) > 1f)
                joystickPosition = Point.Normalize(joystickPosition);
            UpdateKnobPosition(joystickPosition);
            e.Handled = true;
#endif
        }

        private void Joystick_Release(object sender, TouchEventArgs e)
        {
#if NOESIS
            //TODO make more abstract
            //SceneControllerApartment.Instance.IsTouchingUI = false;
#endif
            UpdateKnobPosition(_knobAnchor);
            //SceneControllerApartment.Instance.UnregisterTouch(GetHashCode());
            e.Handled = true;
        }

        private static void JoystickDiameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            InitKnobPosition(dependencyObject);
        }

        private static void KnobDiameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            InitKnobPosition(dependencyObject);
        }

        private void Root_GotTouchCapture(object sender, TouchEventArgs e)
        {
#if NOESIS
            Debug.Log(string.Format("{0} Lost touch", Name));
#endif
        }

        private void Root_LostTouchCapture(object sender, TouchEventArgs e)
        {
#if NOESIS
            Debug.Log(string.Format("{0} Got touch", Name));
#endif
        }

        private void UpdateKnobPosition(Point position)
        {
#if NOESIS
            JoystickX = position.X;
            JoystickY = -position.Y;
            KnobPosition = new Point(position.X * InnerJoystickRadius + JoystickRadius - KnobRadius, position.Y * InnerJoystickRadius + JoystickRadius - KnobRadius);
#endif
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
    }
}