#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    public class VehicleAgent : NavAgentBase
    {
        #region Methods

        #region Regular Methods

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<VehicleAgent>() != null && other.GetComponent<VehicleAgent>().IsPaused)
                IsPaused = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<VehicleAgent>() != null && other.GetComponent<VehicleAgent>().IsPaused == false)
                IsPaused = false;
        }

        #endregion

        #endregion

        //TODO check multiple vehicles
    }
}