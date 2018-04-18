#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#if NOESIS
using Noesis;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/Scripts/UI/SettingsView.xaml");
		}
#endif
    }
}
