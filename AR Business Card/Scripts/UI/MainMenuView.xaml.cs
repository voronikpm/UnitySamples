#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using Noesis;
using Assets.Scripts.ViewModels;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/MainMenuView.xaml");
        }
#endif
    }
}
