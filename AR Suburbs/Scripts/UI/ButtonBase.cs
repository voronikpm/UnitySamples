#region Using Directives

using System;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : UIElement
    {
        #region Properties

        #region Abstract Properties

        public abstract Action Action { get; }

        #endregion

        #endregion

        #region Methods

        #region Virtual Methods

        public virtual void OnClick()
        {
            Action();
        }

        #endregion

        #endregion
    }
}