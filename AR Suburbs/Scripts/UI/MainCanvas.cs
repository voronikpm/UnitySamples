#region Using Directives

using Assets.Scripts.ExtensionMethods;
using UnityEngine;

#endregion

namespace Assets.Scripts.UI
{
    public class MainCanvas : MonoBehaviour
    {
        #region Fields

        #region Static Fields and Constants

        private static MainCanvas _instance;

        #endregion

        #endregion

        #region Properties

        #region Static Properties

        public static MainCanvas Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<MainCanvas>()); }
            set { _instance = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            GetComponentsInChildren<UIElement>(true).InvokeAction(x => x.CheckVisibility());
        }

        #endregion

        #endregion
    }
}