#region Using Directives

using System.Collections.Generic;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.GameObjects;
using UnityEngine;

#endregion

namespace Assets.Scripts.AR
{
    public class SelectiveARObjectMark : ARObjectMark
    {
        #region Fields

        #region  Protected Fields

        [SerializeField]
        protected List<GameObjectBase> _GameObjects;

        #endregion

        #endregion

        #region Methods

        #region Overriding Methods

        protected override void OnTrackingFound()
        {
            _GameObjects.InvokeAction(x => x.IsActive = true);
        }

        protected override void OnTrackingLost()
        {
            _GameObjects.InvokeAction(x => x.IsActive = false);
        }

        #endregion

        #endregion
    }
}