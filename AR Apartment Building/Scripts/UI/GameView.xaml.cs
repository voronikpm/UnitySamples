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

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for GameView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class GameView : UserControl
    {
        #region Constructors

        public GameView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/GameView.xaml");
        }
#endif

        #endregion

        #endregion
    }
}