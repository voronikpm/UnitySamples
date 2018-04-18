using UnityEngine;

namespace Assets.Scripts.AR
{
    public class ARElement : MonoBehaviour
    {
        public virtual bool IsActive
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value);}
        }
    }
}