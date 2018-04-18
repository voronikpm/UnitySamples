using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public abstract class GameObjectBase : MonoBehaviour
    {
        public virtual bool IsActive
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }
    }
}