using Assets.Scripts.Inputs;
using UnityEngine;
using Vuforia;

namespace Assets.Scripts.AR
{
    [RequireComponent(typeof(ImageTargetBehaviour))]
    public class SceneContainer : MonoBehaviour
    {
        public GameObject SecondaryCameraContainer;
        public FirstPersonController CharacterController;
    }
}