#region Using Directives

using System;
using Assets.Scripts.GameObjects.Building;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class DetailsButton : TouchableObject
    {
        #region Properties

        #region Overriding Properties

        public override Action TouchAction
        {
            get { return () => GetComponentInParent<Furniture>().ShowDetails(); }
        }

        #endregion

        #endregion
    }
}