#region Using Directives

using System;
using UnityEngine;

#endregion

namespace Assets.Scripts.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        #region Properties

        #region Abstract Properties

        public abstract Func<bool> VisibilityFunc { get; }

        #endregion

        #region Virtual Properties

        public virtual bool IsActive
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        #endregion

        #endregion

        #region Methods

        #region Virtual Methods

        public virtual void CheckVisibility()
        {
            IsActive = VisibilityFunc();
        }

        #endregion

        #endregion
    }
}