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
using System.Windows.Input;
#endif

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for InfoButtonView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class InfoButtonView : UserControl
    {
        #region Constructors

        public InfoButtonView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/InfoButtonView.xaml");
        }
#endif

        #endregion

        #endregion
    }
}