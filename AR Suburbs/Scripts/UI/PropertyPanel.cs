#region Using Directives

using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.UI
{
    public class PropertyPanel : MonoBehaviour
    {
        #region Fields

        #region  Private Fields

        private VisibleProperty _property;

        #endregion

        #region  Public Fields

        public Text NameText;
        public Text ValueText;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public VisibleProperty Property
        {
            get { return _property; }
            set
            {
                _property = value;
                NameText.text = value.PropertyName;
                ValueText.text = value.PropertyValue;
            }
        }

        #endregion

        #endregion
    }
}