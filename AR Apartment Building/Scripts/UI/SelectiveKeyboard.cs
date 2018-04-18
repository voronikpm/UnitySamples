using Noesis;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class SelectiveKeyboard : UnitySoftwareKeyboard
    {
        protected override TouchScreenKeyboard ShowOnTextBox(TextBox textBox)
        {
            return (string)textBox.Tag == "Number" ? TouchScreenKeyboard.Open(textBox.Text, TouchScreenKeyboardType.PhonePad) : base.ShowOnTextBox(textBox);
        }
    }
}