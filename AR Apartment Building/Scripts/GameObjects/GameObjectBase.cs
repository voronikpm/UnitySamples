#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    public abstract class GameObjectBase : MonoBehaviour
    {
        #region Properties

        #region Virtual Properties

        public virtual bool IsActive
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }

        #endregion

        #endregion
    }
}