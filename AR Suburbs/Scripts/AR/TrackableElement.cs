#region Using Directives

using UnityEngine;
using Vuforia;

#endregion

namespace Assets.Scripts.AR
{
    public class TrackableElement : MonoBehaviour, ITrackableEventHandler
    {
        #region Fields

        #region  Private Fields

        private TrackableBehaviour _trackableBehaviour;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void OnTrackingFound()
        {
            var arElements = GetComponentsInChildren<ARElement>(true);
            foreach (var element in arElements)
                element.IsActive = true;
            Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " found");
        }

        public void OnTrackingLost()
        {
            var arElements = GetComponentsInChildren<ARElement>(true);
            foreach (var element in arElements)
                element.IsActive = false;
            Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
        }

        private void Start()
        {
            _trackableBehaviour = GetComponent<TrackableBehaviour>();
            if (_trackableBehaviour)
                _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
		
		public void Reset()
		{
			//_trackableBehaviour.StopExtendedTracking();
			OnTrackingLost();
			//_trackableBehaviour.StartExtendedTracking();
		}

        #endregion

        #endregion

        #region Interface Implementations

        #region ITrackableEventHandler Members

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
               newStatus == TrackableBehaviour.Status.TRACKED ||
               newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                OnTrackingFound();
            //else
            //    OnTrackingLost();
        }

        #endregion

        #endregion
    }
}