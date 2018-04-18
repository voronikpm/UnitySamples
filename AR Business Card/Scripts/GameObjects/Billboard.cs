using Assets.Scripts.ViewModels;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}