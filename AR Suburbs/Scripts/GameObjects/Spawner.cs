using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI;
using Assets.Scripts.Enums;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Collider))]
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private List<ActorBase> _possibleSpawns;

        [SerializeField]
        private int _maxSpawns = 5;

        [SerializeField]
        [Range(0, 1)]
        private float _spawnChance = 0.5f;

        [SerializeField]
        private float _spawnDelay = 2;

        [SerializeField]
        private float _checkDelay = 0.5f;
        
        [SerializeField]
        private Transform _spawnPosition;
        
        public const int MaxSpawns = 30;

        public static int CurrentSpawns { get; set; }
        private int _currentSpawns;

        private IEnumerator SpawnCoroutine()
        {
            while(true)
            {
                if (CurrentSpawns < MaxSpawns && _currentSpawns < _maxSpawns && Random.value < _spawnChance)
                {
                    var selectedActor = _possibleSpawns.SelectRandomItem();
                    int area = selectedActor.GetComponent<NavMeshAgent>().areaMask;
                    NavMeshHit navMeshPos;
                    var isNavMeshHit = NavMesh.SamplePosition(_spawnPosition.position, out navMeshPos, 1, area);
                    //var actor = Instantiate(selectedActor,_spawnPosition.position,_spawnPosition.rotation,_spawnPosition);
                    if(isNavMeshHit)
                    {
                        var actor = Instantiate(selectedActor, navMeshPos.position, _spawnPosition.rotation, _spawnPosition);
                        actor.transform.parent = transform.parent;
                        actor.transform.localScale = selectedActor.transform.localScale;
                        //yield return new WaitForSeconds(0.5f);
                        yield return new WaitForFixedUpdate();
                        if (actor.GetComponent<PassiveActor>() && actor.GetComponent<ActiveActor>())
                            actor.GetComponent<PassiveActor>().IsInteracting = true;
                        _currentSpawns++;
                        CurrentSpawns++;
                        yield return new WaitForSeconds(_spawnDelay);
                    }
                }
                yield return new WaitForSeconds(_checkDelay);
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<ActorBase>())
            {
                Destroy(other.gameObject);
                CurrentSpawns--;
                _currentSpawns--;
            }
        }
    }
}