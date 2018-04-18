#region Using Directives

using System;
using Assets.Scripts.ViewModels;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class InfoButton : TouchableObject
    {
        #region Fields

        #region  Private Fields

        [SerializeField]
        private bool _isApartment;

        #endregion

        #endregion

        #region Properties

        #region Overriding Properties

        public override Action TouchAction
        {
            get
            {
                return () =>
                       {
                           if(_isApartment)
                               OverlayViewModel.Instance.IsApartmentInfoShown = true;
                           else
                               OverlayViewModel.Instance.IsBuildingInfoShown = true;
                       };
            }
        }

        #endregion

        #endregion
    }
}