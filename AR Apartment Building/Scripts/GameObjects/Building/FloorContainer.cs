#region Using Directives

using System.Collections.Generic;
using System.Linq;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    public class FloorContainer : GameObjectBase
    {
        #region Properties

        #region Regular Properties

        public List<Floor> Floors
        {
            get { return GetComponentsInChildren<Floor>(true).ToList(); }
        }

        #endregion

        #endregion
    }
}