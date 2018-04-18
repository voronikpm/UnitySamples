#region Using Directives

using Assets.Scripts.AR;
using System;

#endregion

namespace Assets.Scripts.UI
{
    public class BackButton : ButtonBase
    {
        #region Properties

        #region Overriding Properties
        public TrackableElement TrackableElement;

        public override Action Action
        {
            get
            {
                return () =>
                {
                    //MainSceneController.Instance.SelectedHouse.ShowInterior(false);
                    //MainSceneController.Instance.SelectedHouse.IsSelected = false;
                    //MainSceneController.IsFirstPerson = false;
                    TrackableElement.OnTrackingLost();
                };
            }
        }

        public override Func<bool> VisibilityFunc
        {
            get { return () => /*MainSceneController.Instance.SelectedHouse || MainSceneController.IsFirstPerson*/ false; }
        }

        #endregion

        #endregion
    }
}