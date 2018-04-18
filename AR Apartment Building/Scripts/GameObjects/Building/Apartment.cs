#region Using Directives

#endregion

using UnityEngine;

namespace Assets.Scripts.GameObjects.Building
{
    public class Apartment : GameObjectBase
    {
        #region Methods

        #region Regular Methods

        //public override Action TouchAction
        //{
        //    //TODO change to SceneControllerCommon
        //    get { return () => SceneControllerHouse.Instance.LoadApartment(); }
        //}

        public void LoadApartment()
        {
            SceneControllerHouse.Instance.ClearController();
            SceneControllerHouse.Instance.LoadApartment();
        }

        #endregion

        #endregion
    }
}