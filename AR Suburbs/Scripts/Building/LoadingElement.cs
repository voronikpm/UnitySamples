using UnityEngine;

namespace Assets.Scripts.Building
{
    public class LoadingElement : MonoBehaviour
    {
        public GameObject NextObject;

        private void Start()
        {
            Instantiate(NextObject);
        }
    }
}