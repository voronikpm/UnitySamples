using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

namespace Assets.Scripts.AR
{
    public class UDTEventHandler : MonoBehaviour, IUserDefinedTargetEventHandler
    {
        #region Fields

        #region Static Fields and Constants

        private static UDTEventHandler _instance;

        #endregion

        #region  Private Fields

        private ImageTargetBuilder.FrameQuality _frameQuality = ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE;
        [SerializeField]
        private ImageTargetBehaviour _imageTargetTemplate;
        private ObjectTracker _objectTracker;
        private UserDefinedTargetBuildingBehaviour _targetBuildingBehaviour;
        private DataSet _udtDataSet;

        #endregion

        #endregion

        #region Properties

        #region Static Properties

        public static UDTEventHandler Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<UDTEventHandler>()); }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void BuildNewTarget()
        {
            //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
            _targetBuildingBehaviour.StartScanning();
            if (_frameQuality != ImageTargetBuilder.FrameQuality.FRAME_QUALITY_NONE)
            {
                _targetBuildingBehaviour.BuildNewTarget(string.Format("{0}_instance", _imageTargetTemplate.TrackableName), _imageTargetTemplate.GetSize().x);
            }
            else
            {
                Debug.Log("Cannot build new target, due to poor camera image quality");
            }
        }

        private void Start()
        {
            _targetBuildingBehaviour = GetComponent<UserDefinedTargetBuildingBehaviour>();
            if (_targetBuildingBehaviour)
            {
                _targetBuildingBehaviour.RegisterEventHandler(this);
                Debug.Log("Registering User Defined Target event handler.");
            }
        }

        #endregion

        #endregion

        #region Interface Implementations

        #region IUserDefinedTargetEventHandler Members

        public void OnFrameQualityChanged(ImageTargetBuilder.FrameQuality frameQuality)
        {
            Debug.Log("Frame quality changed: " + frameQuality);
            _frameQuality = frameQuality;
            if (_frameQuality == ImageTargetBuilder.FrameQuality.FRAME_QUALITY_LOW)
                Debug.Log("Low camera image quality");
        }

        public void OnInitialized()
        {
            _objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
            if (_objectTracker != null)
            {
                _udtDataSet = _objectTracker.CreateDataSet();
                _objectTracker.ActivateDataSet(_udtDataSet);
            }
        }

        public void OnNewTrackableSource(TrackableSource trackableSource)
        {
            _objectTracker.DeactivateDataSet(_udtDataSet);
            _udtDataSet.DestroyAllTrackables(true);
            var instance = Instantiate(_imageTargetTemplate);
            instance.gameObject.name = string.Format("{0}_instance", _imageTargetTemplate.TrackableName);
            instance.gameObject.SetActive(true);
            //TODO make more abstract
            MainSceneController.Instance.SceneContainer = instance.GetComponent<SceneContainer>();
            _udtDataSet.CreateTrackable(trackableSource, instance.gameObject);
            _objectTracker.ActivateDataSet(_udtDataSet);
            StopExtendedTracking();
            _objectTracker.Stop();
            _objectTracker.ResetExtendedTracking();
            _objectTracker.Start();
            _targetBuildingBehaviour.StartScanning();
        }
        private void StopExtendedTracking()
        {
            StateManager stateManager = TrackerManager.Instance.GetStateManager();

            foreach (var tb in stateManager.GetTrackableBehaviours())
            {
                var itb = tb as ImageTargetBehaviour;
                if (itb != null)
                {
                    itb.ImageTarget.StopExtendedTracking();
                }
            }

            List<TrackableBehaviour> trackableList = stateManager.GetTrackableBehaviours().ToList();
            ImageTargetBehaviour lastItb = trackableList.Last() as ImageTargetBehaviour;
            if (lastItb != null)
            {
                if (lastItb.ImageTarget.StartExtendedTracking())
                    Debug.Log("Extended Tracking successfully enabled for " + lastItb.name);
            }
        }
        #endregion

        #endregion
    }
}