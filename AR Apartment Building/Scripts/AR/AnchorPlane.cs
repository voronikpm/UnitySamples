#region Using Directives

using System.Collections.Generic;
using Assets.Scripts.GameObjects;
using UnityEngine;

#endregion

namespace Assets.Scripts.AR
{
    public class AnchorPlane : MonoBehaviour
    {
        #region Fields

        #region  Private Fields

        [SerializeField]
        private List<GameObjectBase> _activatedObjects;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void Awake()
        {
            if(_activatedObjects != null)
                _activatedObjects.ForEach(x => x.IsActive = true);
        }

        #endregion

        #endregion
    }
}