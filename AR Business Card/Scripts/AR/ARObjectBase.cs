#region Using Directives

using Assets.Scripts.EventArgs;
using Assets.Scripts.GameObjects;
using Vuforia;

#endregion

namespace Assets.Scripts.AR
{
    public abstract class ARObjectBase : GameObjectBase
    {
        #region Properties

        #region Virtual Properties

        protected abstract TrackableObject _TrackableObject { get; }

        #endregion

        #endregion

        #region Methods

        #region Event Handlers

        protected virtual void OnTrackingChanged(object sender, TrackingChangedEventHandlerArgs args)
        {
            if(args.NewStatus == TrackableBehaviour.Status.DETECTED || args.NewStatus == TrackableBehaviour.Status.TRACKED || args.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
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