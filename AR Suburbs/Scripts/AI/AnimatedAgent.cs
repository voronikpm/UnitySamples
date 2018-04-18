using System.Collections;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class AnimatedAgent : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private Vector2 _velocity;
        private Vector2 _smoothDeltaPosition;

        [SerializeField]
        private bool _isVehicle;

        [SerializeField]
        private float _angularSpeed = 2;
        

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updatePosition = false;

            if (_isVehicle)
                _agent.updateRotation = false;
        }

        private void Update()
        {
            Vector3 worldDeltaPosition = _agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            // Low-pass filter the deltaMove
            float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
            _smoothDeltaPosition = Vector2.Lerp(_smoothDeltaPosition, deltaPosition, smooth);

            // Update velocity if time advances
            if (Time.deltaTime > float.Epsilon)
                _velocity = _smoothDeltaPosition / Time.deltaTime;

            bool shouldMove = _velocity.magnitude > 0.5f && _agent.remainingDistance > _agent.radius;
            // Update animation parameters
            _animator.SetBool(string.Format("Should{0}",GetComponent<ActiveActor>().MovementType.ToString()), shouldMove);

            _animator.SetFloat("VelX", _velocity.x);
            _animator.SetFloat("VelY", _velocity.y);

            if(_isVehicle)
            {
                Vector3 relativePos = new Vector3(_agent.steeringTarget.x, transform.position.y, _agent.steeringTarget.z) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos);
                if (Time.deltaTime > float.Epsilon)
                    //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _angularSpeed);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * _angularSpeed);
            }
        }
        
        private void OnAnimatorMove()
        {
            if(!_agent)
                _agent = GetComponent<NavMeshAgent>();
            transform.position = _agent.nextPosition;
        }
    }
}