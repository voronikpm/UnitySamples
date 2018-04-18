using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Collider))]
    public class ParkingPoint : MonoBehaviour
    {
        private GameObject _parkedVehicle;
        
        private void OnTriggerStay(Collider other)
        {
            if(_parkedVehicle == null && other.GetComponent<NavMeshObstacle>())
            {
                _parkedVehicle = other.gameObject;
                GetComponentInParent<PassiveActor>().IsInteracting = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject == _parkedVehicle)
            {
                _parkedVehicle = null;
                GetComponentInParent<PassiveActor>().IsInteracting = false;
            }
        }

    }
}