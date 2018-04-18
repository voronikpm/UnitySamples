#region Using Directives

using System.Collections.Generic;
using Assets.Scripts.EventArgs;
using Assets.Scripts.GameObjects;
using Vuforia;

#endregion

namespace Assets.Scripts.AR
{
    public abstract class ARObjectBase : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        private readonly List<TrackableBehaviour.Status> _badStatuses = new List<TrackableBehaviour.Status> {TrackableBehaviour.Status.DEGRADED, TrackableBehaviour.Status.NOT_FOUND};
        private readonly List<TrackableBehaviour.Status> _goodStatuses = new List<TrackableBehaviour.Status> {TrackableBehaviour.Status.DETECTED, TrackableBehaviour.Status.TRACKED, TrackableBehaviour.Status.EXTENDED_TRACKED};

        #endregion

        #endregion

        #region Properties

        #region Abstract Properties

        protected abstract TrackableObject _TrackableObject { get; }

        #endregion

        #endregion

        #region Methods

        #region Event Handlers

        protected virtual void OnTrackingChanged(object sender, TrackingChangedEventHandlerArgs args)
        {
            if(_goodStatuses.Contains(args.PreviousStatus) && _goodStatuses.Contains(args.NewStatus) || _badStatuses.Contains(args.PreviousStatus) && _badStatuses.Contains(args.NewStatus))
                return;
            if(_goodStatuses.Contains(args.NewStatus))
                OnTrackingFound();
            else
                OnTrackingLost();
        }

        #endregion

        #region Virtual Methods

        protected virtual void Awake()
        {
            _TrackableObject.OnTrackingChanged += OnTrackingChanged;
        }

        protected virtual void OnTrackingFound()
        {
        }

        protected virtual void OnTrackingLost()
        {
        }

        #endregion

        #endregion
    }
}