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
    /// Interaction logic for ChatMessageView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class ChatMessageView : UserControl
    {
        public ChatMessageView()
        {
            InitializeComponent();
        }

#if NOESIS
		private void InitializeComponent()
		{
			Noesis.GUI.LoadComponent(this, "Assets/Scripts/UI/ChatMessageView.xaml");
		}
#endif
    }
}
