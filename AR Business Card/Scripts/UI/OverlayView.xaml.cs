#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
#else
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#endif
using System;
using System.Reflection;
using Assets.Scripts.ViewModels;

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for OverlayView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class OverlayView : UserControl
    {

#if NOESIS
        private Grid _splashScreenGrid;
#endif

        #region Constructors

        public OverlayView()
        {
            Initialized += OnInitialized;
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/OverlayView.xaml");
            _splashScreenGrid = FindName("_splashScreenGrid") as Grid;
        }
#endif

        #endregion

        #region Event Handlers

#if NOESIS
        private void OnInitialized(object sender, Noesis.EventArgs args)
        {
            DataContext = OverlayViewModel.Instance;
        }
#else
        private void OnInitialized(object sender, EventArgs args)
        {
            DataContext = new OverlayViewModel();
        }
#endif

        #endregion

        #endregion

        private void Grid_TouchUp(object sender, TouchEventArgs e)
        {
            _splashScreenGrid.Visibility = Visibility.Collapsed;
#if NOESIS
            //TODO make more abstract
            TurbineViewModel.Instance.IsTargetContainerShown = true;
#endif
        }

        private void UserControl_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {

        }


#if NOESIS
        protected override void Connect(object source, string eventName, string handlerName)
        {
            var eventInfo = source.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.Instance);
            if (eventInfo == null)
                return;
            var methodInfo = GetType().GetMethod(handlerName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
                return;
            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
            eventInfo.AddEventHandler(source, handler);
        }
#endif
    }
}