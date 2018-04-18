using System;
using Assets.Scripts.AR;
using UnityEngine;
using Vuforia;

namespace Assets.Scripts.UI
{
    public class ScanButton : ButtonBase
    {
        private bool _hasScanned;
        [SerializeField]
        private SceneContainer _imageTarget;
        public override Func<bool> VisibilityFunc
        {
            get { return () => !_hasScanned; }
        }

        public override Action Action
        {
            get
            {
                return () =>
                       {
                           //TODO temporary
                           _imageTarget.gameObject.SetActive(true);
                           MainSceneController.Instance.SceneContainer = _imageTarget;
                           //UDTEventHandler.Instance.BuildNewTarget();
                           _hasScanned = true;
                       };
            }
        }
    }
}