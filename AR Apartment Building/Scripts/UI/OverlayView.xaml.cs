#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif
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
    }
}