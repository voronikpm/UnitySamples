#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif

#region Using Directives

#if NOESIS
using System;
using Noesis;
using Assets.Scripts.ViewModels;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif
using System.Reflection;

#endregion

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TurbineInfo : UserControl
    {
        public TurbineInfo()
        {
            InitializeComponent();
        }
        
#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/TurbineInfo.xaml");
        }
#endif
    }

}
