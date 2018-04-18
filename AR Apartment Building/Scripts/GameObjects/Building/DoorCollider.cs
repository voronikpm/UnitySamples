#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    [RequireComponent(typeof(Collider))]
    public class DoorCollider : MonoBehaviour
    {
        #region Fields

        #region  Public Fields

        public Door Door;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void OnTriggerEnter(Collider other)
        {
            Door.OpenDoor();
        }

        private void OnTriggerExit(Collider other)
        {
            Door.CloseDoor();
        }

        #endregion

        #endregion
    }
}