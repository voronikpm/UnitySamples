#region Using Directives

using System;
using Assets.Scripts.GameObjects.Building;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class EnterButton : TouchableObject
    {
        #region Properties

        #region Overriding Properties

        public override Action TouchAction
        {
            //get { return () => GetComponentInParent<Apartment>().LoadApartment(); }
            get
            {
                return () =>
                       {

                           SceneControllerHouse.Instance.ClearController();
                           SceneControllerHouse.Instance.LoadApartment();
                       };
            }
        }

        #endregion

        #endregion
    }
}