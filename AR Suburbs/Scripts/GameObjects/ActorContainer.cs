using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Collider))]
    public class ActorContainer : MonoBehaviour
    {
        private string _actorName;
        private ActiveActor _possibleActor;

        public List<ActiveActor> FallbackActors;

        [SerializeField]
        private bool _isInUse;

        private void OnTriggerEnter(Collider other)
        {
            if(_isInUse)
                return;
            var actor = other.GetComponent<ActiveActor>();
            if(actor && GetComponent<PassiveActor>().InteractionPoints.Contains(actor.CurrentInteractionPoint))
                _possibleActor = actor;
        }

        private void OnTriggerExit(Collider other)
        {
            //if(other.GetComponent<ActiveActor>() == _possibleActor)
            //    _possibleActor = null;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isInUse)
                return;
            var actor = other.GetComponent<ActiveActor>();
            if (actor && GetComponent<PassiveActor>().InteractionPoints.Contains(actor.CurrentInteractionPoint))
                _possibleActor = actor;
        }

        private void DestroyActor()
        {
            if(!_possibleActor)
                return;
            var actorName = GetActorName(_possibleActor);
            _actorName = actorName;
            Destroy(_possibleActor.gameObject);
            _isInUse = true;
        }
        
        public void ReleaseActor()
        {
            var actor = !string.IsNullOrEmpty(_actorName) ? FallbackActors.FirstOrDefault(x => x.name == _actorName) : FallbackActors.SelectRandomItem();
            if (!actor)
                return;
            _possibleActor = null;
            var spawnedActor = Instantiate(actor, transform.position, transform.rotation, transform);
            spawnedActor.transform.parent = transform.parent;
            spawnedActor.transform.localScale = actor.transform.localScale;
            //TODO make more abstract
            spawnedActor.GetComponent<ActiveActor>().EndInteraction("EnterCar");
            _isInUse = false;
        }

        private string GetActorName(ActiveActor actor)
        {
            return actor.name.EndsWith("(Clone)") ? actor.name.Substring(0, actor.name.Length - 7) : actor.name;
        }
    }
}