#region Using Directives

using Vuforia;

#endregion

namespace Assets.Scripts.EventArgs
{
    public class TrackingChangedEventHandlerArgs
    {
        #region Constructors

        public TrackingChangedEventHandlerArgs(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            PreviousStatus = previousStatus;
            NewStatus = newStatus;
        }

        #endregion

        #region Properties

        #region Regular Properties

        public TrackableBehaviour.Status NewStatus { get; private set; }
        public TrackableBehaviour.Status PreviousStatus { get; private set; }

        #endregion

        #endregion
    }
}