#region Using Directives

using Assets.Scripts.EventArgs;
using UnityEngine;
using Vuforia;

#endregion

namespace Assets.Scripts.AR
{
    public class TrackableObject : MonoBehaviour, ITrackableEventHandler
    {
        #region Public Delegates

        public delegate void TrackingChangedEventHandler(object sender, TrackingChangedEventHandlerArgs args);

        #endregion

        #region Events

        public event TrackingChangedEventHandler OnTrackingChanged;

        #endregion

        #region Fields

        #region  Private Fields

        private TrackableBehaviour _trackableBehaviour;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void Start()
        {
            _trackableBehaviour = GetComponent<TrackableBehaviour>();
            if(_trackableBehaviour)
                _trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        #endregion

        #endregion

        #region Interface Implementations

        #region ITrackableEventHandler Members

        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            //Debug.Log(string.Format("Trackable {0} changed from {1} to {2}", _trackableBehaviour.TrackableName, previousStatus, newStatus));
            if(OnTrackingChanged != null)
                OnTrackingChanged(this, new TrackingChangedEventHandlerArgs(previousStatus, newStatus));
        }

        #endregion

        #endregion
    }
}