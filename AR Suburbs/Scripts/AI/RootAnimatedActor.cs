using UnityEngine;

namespace Assets.Scripts.AI
{
    [RequireComponent(typeof(Animator))]
    public class RootAnimatedActor : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            if(!_animator)
                _animator = GetComponent<Animator>();
            transform.position = _animator.rootPosition;
            transform.rotation = _animator.rootRotation;
        }

        //private void OnEnable()
        //{
        //    _animator.applyRootMotion = true;
        //}

        //private void OnDisable()
        //{

        //}

    }
}