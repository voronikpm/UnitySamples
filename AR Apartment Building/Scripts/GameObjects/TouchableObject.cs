#region Using Directives

using System;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Collider))]
    public class TouchableObject : GameObjectBase
    {
        #region Properties

        #region Virtual Properties

        public virtual Action TouchAction { get; protected set; }

        #endregion

        #endregion
    }
}