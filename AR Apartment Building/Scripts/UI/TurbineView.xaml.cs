﻿#if UNITY_5_3_OR_NEWER
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

        #endregion

        #region Methods

        #region Regular Methods

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/TurbineView.xaml");
        }
#endif

        #endregion

        #endregion
    }
}