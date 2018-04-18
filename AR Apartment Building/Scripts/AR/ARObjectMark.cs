#region Using Directives

using System.Linq;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.GameObjects;
using UnityEngine;

#endregion

namespace Assets.Scripts.AR
{
    [RequireComponent(typeof(TrackableObject))]
    public class ARObjectMark : ARObjectBase
    {
        #region Properties

        #region Overriding Properties

        protected override TrackableObject _TrackableObject
        {
            get { return GetComponent<TrackableObject>(); }
        }

        #endregion

        #endregion

        #region Methods

        #region Overriding Methods

        protected override void OnTrackingFound()
        {
            GetComponentsInChildren<GameObjectBase>(true).Where(x => x.gameObject != gameObject).InvokeAction(x => x.IsActive = true);
            Debug.Log("Trackable found");
        }

        protected override void OnTrackingLost()
        {
            GetComponentsInChildren<GameObjectBase>(true).Where(x => x.gameObject != gameObject).InvokeAction(x => x.IsActive = false);
            Debug.Log("Trackable lost");
        }

        #endregion

        #endregion
    }
}