#region Using Directives

using System;
using UnityEngine;
using Vuforia;

#endregion

namespace Assets.Scripts.AR
{
    public class PlanePositioner : MonoBehaviour
    {
        #region Fields

        #region  Private Fields

        private PositionalDeviceTracker _deviceTracker;
        private GameObject _previousAnchor;

        #endregion

        #region  Public Fields

        public GameObject AnchorStage;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Awake()
        {
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        }

        public void OnDestroy()
        {
            VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
        }

        public void OnInteractiveHitTest(HitTestResult result)
        {
            if(result == null || AnchorStage == null)
            {
                Debug.LogWarning("Hit test is invalid or AnchorStage not set");
                return;
            }
            var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);
            if(anchor != null)
            {
                AnchorStage.transform.parent = anchor.transform;
                AnchorStage.transform.localPosition = Vector3.zero;
                AnchorStage.transform.localRotation = Quaternion.identity;
                AnchorStage.SetActive(true);
            }
            if(_previousAnchor != null)
                Destroy(_previousAnchor);
            _previousAnchor = anchor;
        }

        public void Start()
        {
            if(AnchorStage == null)
            {
                Debug.Log("AnchorStage must be specified");
                return;
            }
            AnchorStage.SetActive(false);
        }

        private void OnVuforiaStarted()
        {
            _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
        }

        #endregion

        #endregion
    }
}