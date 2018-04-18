#region Using Directives

using Assets.Scripts.AR;

#endregion

namespace Assets.Scripts.EventArgs
{
    public class TargetBuiltEventArgs
    {
        #region Constructors

        public TargetBuiltEventArgs(TrackableObject trackable)
        {
            TrackableObject = trackable;
        }

        #endregion

        #region Properties

        #region Regular Properties

        public TrackableObject TrackableObject { get; private set; }

        #endregion

        #endregion
    }
}