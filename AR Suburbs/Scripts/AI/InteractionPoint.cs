using UnityEngine;

namespace Assets.Scripts.AI
{
    public class InteractionPoint : MonoBehaviour
    {
        private bool _isFree = true;

        public bool IsFree
        {
            get { return _isFree; }
            set { _isFree = value; }
        }

        public Vector3 Position { get { return transform.position; } }
        public Quaternion Rotation { get { return transform.rotation; } }
    }
}