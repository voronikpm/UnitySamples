#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class SceneControllerTurbine : SceneControllerBase
    {
        #region Fields

        #region Static Fields and Constants

        private static bool _isMuted;

        #endregion

        #region  Private Fields

        [SerializeField]
        private AudioSource _audioSource;

        private TurbineAnimationType _currentAnimationType = TurbineAnimationType.None;
        private TurbineAnimationType _delayedAnimationType = TurbineAnimationType.None;

        [SerializeField]
        private AudioClip _disassembleClip;

        private bool _isInfoShown;

        [SerializeField]
        private AudioClip _loopClip;

        private Coroutine _startAudioCoroutine;

        [SerializeField]
        private AudioClip _startClip;

        [SerializeField]
        private AudioClip _stopClip;

        [SerializeField]
        private List<AnimationController> AnimationControllers;

        #endregion

        #endregion

        #region Constructors

        public SceneControllerTurbine()
        {
            SceneState = TurbineState.Default;
        }

        #endregion

        #region Properties

        #region Regular Properties

        public bool IsAnimating { get; private set; }

        public bool IsMuted
        {
            get { return _audioSource == null || _audioSource.mute; }
            set
            {
                if(_audioSource)
                    _audioSource.mute = value;
            }
        }

        public TurbineState SceneState { get; set; }

        #endregion

        #region Static Properties

        public static SceneControllerTurbine Instance { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Mute(bool? forcedState)
        {
            bool value = forcedState ?? !_audioSource.mute;
            _isMuted = value;
            if(_audioSource)
                _audioSource.mute = value;
        }

        public void OnAnimationEnd()
        {
            IsAnimating = false;
            switch(_currentAnimationType)
            {
                case TurbineAnimationType.Cutoff:
                    StartAnimations(TurbineAnimationType.Rotate);
                    SceneState = TurbineState.Cut;
                    break;
                case TurbineAnimationType.Rotate:
                    break;
                case TurbineAnimationType.Move:
                    SceneState = TurbineState.Exploded;
                    break;
                case TurbineAnimationType.None:
                    break;
                case TurbineAnimationType.RevertCutoff:
                    EndReverseAnimation();
                    break;
                case TurbineAnimationType.StopRotation:
                    StartAnimations(TurbineAnimationType.RevertCutoff);
                    break;
                case TurbineAnimationType.RevertMove:
                    EndReverseAnimation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StartAnimations(TurbineAnimationType type)
        {
            if(type != TurbineAnimationType.RevertCutoff)
                _delayedAnimationType = TurbineAnimationType.None;
            switch(SceneState)
            {
                case TurbineState.Default:
                    break;
                case TurbineState.Cut:
                    if(_currentAnimationType != TurbineAnimationType.StopRotation)
                    {
                        _delayedAnimationType = type;
                        type = TurbineAnimationType.StopRotation;
                    }
                    break;
                case TurbineState.Exploded:
                    _delayedAnimationType = type;
                    type = TurbineAnimationType.RevertMove;
                    break;
            }
            _currentAnimationType = type;
            if(type != TurbineAnimationType.Rotate && type != TurbineAnimationType.None)
                IsAnimating = true;
            AnimationControllers.ForEach(x => x.Play(type));
            PlaySound(type);
        }

        private void EndReverseAnimation()
        {
            _currentAnimationType = TurbineAnimationType.None;
            SceneState = TurbineState.Default;
            if(_delayedAnimationType != TurbineAnimationType.None)
                StartAnimations(_delayedAnimationType);
        }

        private void PlaySound(TurbineAnimationType type)
        {
            switch(type)
            {
                case TurbineAnimationType.Rotate:
                {
                    _startAudioCoroutine = StartCoroutine(StartAudioCoroutine());
                    break;
                }
                case TurbineAnimationType.Move:
                {
                    if(_audioSource != null)
                        _audioSource.PlayOneShot(_disassembleClip);
                    break;
                }
                case TurbineAnimationType.StopRotation:
                {
                    StopCoroutine(_startAudioCoroutine);
                    if(_audioSource != null)
                    {
                        _audioSource.Stop();
                        _audioSource.PlayOneShot(_stopClip);
                    }
                    break;
                }
            }
        }

        private IEnumerator StartAudioCoroutine()
        {
            if(_audioSource != null)
            {
                _audioSource.PlayOneShot(_startClip);
                yield return new WaitForSeconds(_startClip != null ? _startClip.length : 0);
                _audioSource.Stop();
                _audioSource.clip = _loopClip;
                _audioSource.Play();
            }
        }

        #endregion

        #region Overriding Methods

        protected override void Awake()
        {
            Instance = FindObjectOfType<SceneControllerTurbine>();
            base.Awake();
            Mute(_isMuted);
        }

        #endregion

        #endregion

        //private static SceneControllerTurbine _instance;
    }
}