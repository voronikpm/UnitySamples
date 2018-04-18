using UnityEngine;
using Vuforia;

namespace Assets.Scripts.AR
{
    public class ARCameraFocus : MonoBehaviour
    {
        private void Start()
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}