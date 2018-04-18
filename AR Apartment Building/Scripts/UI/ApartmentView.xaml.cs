#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

using System.Reflection;
using Assets.Scripts.ViewModels;
#if NOESIS
using System;
using Noesis;
using UnityEngine;
using GUI = Noesis.GUI;
#else
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
#endif

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    ///     Interaction logic for ApartmentView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class ApartmentView : UserControl
    {
        public ApartmentView()
        {
            //TODO Remove in the merged version
            Initialized += OnInitialized;
            InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/ApartmentView.xaml");
        }
#endif
#if NOESIS
        //TODO Remove in the merged version
        private void OnInitialized(object sender, Noesis.EventArgs args)
        {
            DataContext = ApartmentViewModel.Instance;
        }
#else //TODO Remove in the merged version
        private void OnInitialized(object sender, EventArgs args)
        {
        }
#endif
    }
}