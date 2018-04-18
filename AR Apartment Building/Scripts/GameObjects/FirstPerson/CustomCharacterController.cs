#region Using Directives

using Assets.Scripts.Helpers;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class CustomCharacterController : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        private CharacterController _characterController;

        [SerializeField]
        private MouseLook _mouseLook;

        private Vector3 _moveDir = Vector3.zero;
        
        [SerializeField]
        private float _walkSpeed;

        [SerializeField] private float _stepInterval;
        [SerializeField] private AudioClip[] _footstepSounds;
        private AudioSource _audioSource;
        private float _stepCycle;
        private float _nextStep;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void RotateView()
        {
            _mouseLook.LookRotation(transform);
        }

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _mouseLook.Init(transform);
            _stepCycle = 0f;
            _nextStep = _stepCycle / 2f;
            _audioSource = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            var desiredMove = transform.forward * CustomInput.Axes["Move Y"] + transform.right * CustomInput.Axes["Move X"];
            _moveDir.x = desiredMove.x * _walkSpeed;
            _moveDir.z = desiredMove.z * _walkSpeed;
            _characterController.Move(_moveDir * Time.fixedDeltaTime);
            var character = _characterController.transform;
            RotateView();
            _characterController.transform.localRotation = character.localRotation;
            _characterController.GetComponentInChildren<Camera>().transform.localRotation = Camera.main.transform.localRotation;

            ProgressStepCycle(1);
        }

        private void ProgressStepCycle(float speed)
        {
            if (_characterController.velocity.sqrMagnitude > 0 && (_moveDir.x != 0 || _moveDir.y != 0))
            {
                _stepCycle += (_characterController.velocity.magnitude + speed) * Time.fixedDeltaTime;
            }

            if (!(_stepCycle > _nextStep))
            {
                return;
            }

            _nextStep = _stepCycle + _stepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            int n = Random.Range(1, _footstepSounds.Length);
            _audioSource.clip = _footstepSounds[n];
            _audioSource.PlayOneShot(_audioSource.clip);
            _footstepSounds[n] = _footstepSounds[0];
            _footstepSounds[0] = _audioSource.clip;
        }

        #endregion

        #endregion
    }
}