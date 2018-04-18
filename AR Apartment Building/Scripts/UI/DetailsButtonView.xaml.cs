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
    ///     Interaction logic for DetailsButtonView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class DetailsButtonView : UserControl
    {
        #region Constructors

        public DetailsButtonView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/DetailsButtonView.xaml");
        }
#endif

        #endregion

        #endregion
    }
}