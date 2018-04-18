using Assets.Scripts.AI;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Collider))]
    public class BoundaryTrigger : MonoBehaviour
    {

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<ActorBase>())
            {
                Destroy(other.gameObject);
                Spawner.CurrentSpawns--;
            }
        }

    }
}