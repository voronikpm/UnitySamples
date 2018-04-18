#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif
#if NOESIS

#region Using Directives

using System;
using System.Reflection;
using Noesis;
using UnityEngine;
using GUI = Noesis.GUI;

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
    ///     Interaction logic for ChatConrolView.xaml
    /// </summary>
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class ChatControlView : UserControl
    {
        #region Fields

        #region  Private Fields

#if NOESIS
        private TextBox _entryText;
#endif
#if NOESIS
        private TextBlock _placeholderText;
#endif

        #endregion

        #endregion

        #region Constructors

        public ChatControlView()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Regular Methods

        private void _sendButton_GotFocus(object sender, RoutedEventArgs e)
        {
            _placeholderText.Visibility = Visibility.Collapsed;
            //var textBox = sender as TextBox;
            //if(textBox == null)
            //    return;
            //var showKeyboardResult = GUI.SoftwareKeyboard.Show(textBox);
            //UnityEngine.Debug.Log(string.Format("textbox: {0};keyboard: {1}, result: {2}",textBox, GUI.SoftwareKeyboard, showKeyboardResult));
            //if(!showKeyboardResult)
            //{
            //    var keyboard = UnityEngine.TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
            //    UnityEngine.Debug.Log(string.Format("supported: {0}; active: {1}; status {2}", UnityEngine.TouchScreenKeyboard.isSupported, keyboard.active, keyboard.status));
            //}
        }

        private void _sendButton_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(_entryText.Text))
                _placeholderText.Visibility = Visibility.Visible;
        }

#if NOESIS
        private void InitializeComponent()
        {
            GUI.LoadComponent(this, "Assets/Scripts/UI/ChatControlView.xaml");
            _placeholderText = FindName("_placeholderText") as TextBlock;
            _entryText = FindName("_entryText") as TextBox;
        }
#endif

        #endregion

        #region Overriding Methods

#if NOESIS
        protected override void Connect(object source, string eventName, string handlerName)
        {
            var eventInfo = source.GetType().GetEvent(eventName, BindingFlags.Public | BindingFlags.Instance);
            if(eventInfo == null)
                return;
            var methodInfo = GetType().GetMethod(handlerName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if(methodInfo == null)
                return;
            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
            eventInfo.AddEventHandler(source, handler);
        }
#endif

        #endregion

        #endregion
    }
}