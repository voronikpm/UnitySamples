#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif
#if NOESIS

#region Using Directives

using Noesis;

#endregion

#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for FurnitureView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class FurnitureView : UserControl
    {
        #region Constructors

        public FurnitureView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/FurnitureView.xaml");
        }
#endif

        #endregion

        #endregion
    }
}