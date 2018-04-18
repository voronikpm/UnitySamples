#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System;
using System.Reflection;
using Assets.Scripts.GameObjects;
using Assets.Scripts.ViewModels;
using Noesis;
using UnityEngine;
using Canvas = Noesis.Canvas;
using GUI = Noesis.GUI;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
#endif

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for TurbineView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class TurbineView : UserControl
    {
        #region Constructors

        public TurbineView()
        {
            InitializeComponent();
        }

#if NOESIS
        private Canvas _renderTextureCanvas;
#endif

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/TurbineView.xaml");
            _renderTextureCanvas = (Canvas) FindName("_renderTextureCanvas");
            TurbineViewModel.Instance.RenderTextureCanvas = _renderTextureCanvas;
        }
#endif

        #endregion
        
        #endregion

        private void _renderTextureCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
#if NOESIS
            SceneControllerTurbine.Instance.RotateElementGroup(e.DeltaManipulation.Translation);
#endif
        }

        private void ZoomCanvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
#if NOESIS
            //SceneControllerTurbine.Instance.Zoom(e.DeltaManipulation.Expansion);
#endif
        }

        private void ZoomCanvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            //e.Mode = ManipulationModes.Scale;
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