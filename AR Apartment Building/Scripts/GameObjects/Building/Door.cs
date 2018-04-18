#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class Door : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        private Animator _animator;
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _closeDoorClip;

        private bool _isOpened;

        [SerializeField]
        private AudioClip _openDoorClip;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void CloseDoor()
        {
            _animator.Play("DoorClose");
            _audioSource.PlayOneShot(_closeDoorClip);
            _isOpened = false;
        }

        public void OpenDoor()
        {
            _animator.Play("DoorOpen");
            _audioSource.PlayOneShot(_openDoorClip);
            _isOpened = true;
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _animator = GetComponent<Animator>();
        }

        #endregion

        #endregion
    }
}