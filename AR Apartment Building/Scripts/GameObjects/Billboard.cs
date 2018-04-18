#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class Billboard : MonoBehaviour
    {
        #region Methods

        #region Regular Methods

        private void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }

        #endregion

        #endregion
    }
}