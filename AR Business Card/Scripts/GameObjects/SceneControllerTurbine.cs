#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AR;
using Assets.Scripts.Enums;
using Assets.Scripts.EventArgs;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.ViewModels;
using Noesis;
using UnityEngine;
using Vuforia;
using Vector3 = UnityEngine.Vector3;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class SceneControllerTurbine : SceneControllerBase
    {
        #region Fields

        #region  Private Fields

        private TurbineAnimationType _currentAnimationType = TurbineAnimationType.None;
        private TurbineAnimationType _delayedAnimationType = TurbineAnimationType.None;

        [SerializeField]
        private AudioClip _disassembleClip;

        [SerializeField]
        private List<TurbineElementGroup> _elementGroups;

        private bool _isAnimating;
        private bool _isInfoShown;

        [SerializeField]
        private AudioClip _loopClip;

        private TurbineState _sceneState;
        private Coroutine _startAudioCoroutine;

        [SerializeField]
        private AudioClip _startClip;

        [SerializeField]
        private AudioClip _stopClip;

        private TrackableObject _trackable;
        private MonoBehaviour _turbine;

        #endregion

        #region  Public Fields

        public Camera AssemblyCamera;

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

        public bool IsAnimating
        {
            get { return _isAnimating; }
            private set
            {
                _isAnimating = value;
                TurbineViewModel.Instance.RefreshCommands();
            }
        }

        public TurbineState SceneState
        {
            get { return _sceneState; }
            set
            {
                _sceneState = value;
                if(_shouldUpdateViewModelState)
                {
                    TurbineViewModel.Instance.SceneState = value;
                }
                TurbineViewModel.Instance.RefreshCommands();
            }
        }

        #endregion

        #region Static Properties

        public static SceneControllerTurbine Instance { get; private set; }

        #endregion

        #region Overriding Properties

        public override SceneType ControllerSceneType => SceneType.Turbine;

        public override bool IsMuted
        {
            get
            {
                var audioSource = FindObjectOfType<AudioSource>();
                return audioSource == null || audioSource.mute;
            }
            set
            {
                var audioSource = FindObjectOfType<AudioSource>();
                if(audioSource)
                    audioSource.mute = value;
                _Settings.IsMuted = value;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void HookOnTrackingChanged()
        {
            _trackable = FindObjectOfType<TrackableObject>();
            if(_trackable)
            {
                _trackable.OnTrackingChanged += _trackable_OnTrackingChanged;
                _turbine = _trackable.GetComponentInChildren<AnimationController>(true);
            }
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

        public void SelectElement(TurbineElement element)
        {
            _elementGroups.ForEach(x => x.IsActive = false);
            if(element != TurbineElement.None)
            {
                _turbine.gameObject.SetActive(false);
                SelectedElementGroup = _elementGroups.First(x => x.Element == element);
                SelectedElementGroup.IsActive = true;
            }
            else
            {
                SelectedElementGroup.IsActive = false;
                _turbine.gameObject.SetActive(true);
            }
        }

        public TurbineElementGroup SelectedElementGroup { get; private set; }

        public void SetRenderTexture()
        {
            var renderTexture = new RenderTexture(512, 512, 1, RenderTextureFormat.Default);
            RenderTexture.active = renderTexture;

            // Set render texture as camera target
            Instance.AssemblyCamera.targetTexture = renderTexture;
            Instance.AssemblyCamera.aspect = 1;

            // Create brush to store render texture
            var texBrush = new ImageBrush
                           {
                               ImageSource = new TextureSource(renderTexture),
                               Stretch = Stretch.UniformToFill,
                               Opacity = 1
                           };
            TurbineViewModel.Instance.RenderTextureCanvas.Background = texBrush;
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
            FindObjectsOfType<AnimationController>().InvokeAction(x => x.Play(type));
            PlaySound(type);
        }

        public void Zoom(Point delta)
        {
            if(!SceneControllerCommon.Instance.IsMultiTouch)
                return;
            if (Math.Abs(delta.X) < float.Epsilon && Math.Abs(delta.Y) < float.Epsilon)
                return;
            Debug.Log(delta);
            var value = (delta.X + delta.Y)/10;
            var isFloat = !float.IsNaN(value) && !float.IsInfinity(value);
            if(isFloat)
                _turbine.transform.localScale *= value;
        }

        public void Zoom(Vector2 delta)
        {
            if (!SceneControllerCommon.Instance.IsMultiTouch)
                return;
            if (Math.Abs(delta.x) < float.Epsilon && Math.Abs(delta.y) < float.Epsilon)
                return;
            Debug.Log(delta);
            var value = (delta.x + delta.y) / 10;
            var isFloat = !float.IsNaN(value) && !float.IsInfinity(value);
            if (isFloat)
                _turbine.transform.localScale *= (1 + value);
        }

        public void Zoom(Touch t1, Touch t2)
        {
            if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
            {
                var t1p = t1.position - t1.deltaPosition;
                var t2p = t2.position - t2.deltaPosition;
                var delta = (t1.position - t2.position).magnitude - (t1p - t2p).magnitude;
                delta /= 100;
                if(delta <= -1)
                    delta = -0.9f;
                _turbine.transform.localScale *= (1 + delta);
            }
        }

        public void RotateElementGroup(Point value)
        {
            value /= 2;
            var isFloatX = !float.IsNaN(value.X) && !float.IsInfinity(value.X);
            SelectedElementGroup.transform.RotateAround(Vector3.zero, Vector3.down, isFloatX ? value.X : 0);
        }

        private void EndReverseAnimation()
        {
            _currentAnimationType = TurbineAnimationType.None;
            _shouldUpdateViewModelState = _delayedAnimationType == TurbineAnimationType.None;
            SceneState = TurbineState.Default;
            if(_delayedAnimationType != TurbineAnimationType.None)
                StartAnimations(_delayedAnimationType);
        }

        //TODO make less shitty
        private bool _shouldUpdateViewModelState = true;

        private void OnTrackingChanged(bool isTracked)
        {
            TurbineViewModel.Instance.IsTargetShown = !isTracked;
            TurbineViewModel.Instance.RefreshProperties();
            IsMuted = _Settings.IsMuted; //in case of new audiosource
        }

        private void PlaySound(TurbineAnimationType type)
        {
            var audioSource = FindObjectOfType<AudioSource>();
            switch(type)
            {
                case TurbineAnimationType.Rotate:
                {
                    _startAudioCoroutine = StartCoroutine(StartAudioCoroutine());
                    break;
                }
                case TurbineAnimationType.Move:
                {
                    if(audioSource && audioSource.gameObject.activeInHierarchy)
                        audioSource.PlayOneShot(_disassembleClip);
                    break;
                }
                case TurbineAnimationType.StopRotation:
                {
                    if(_startAudioCoroutine != null)
                        StopCoroutine(_startAudioCoroutine);
                    if(audioSource != null && audioSource.gameObject.activeInHierarchy)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(_stopClip);
                    }
                    break;
                }
            }
        }

        private IEnumerator StartAudioCoroutine()
        {
            var audioSource = FindObjectOfType<AudioSource>();
            if(audioSource && audioSource.gameObject.activeInHierarchy)
            {
                audioSource.PlayOneShot(_startClip);
                yield return new WaitForSeconds(_startClip?.length ?? 0);
                if(audioSource != null && audioSource.gameObject.activeInHierarchy)
                {
                    audioSource.Stop();
                    audioSource.clip = _loopClip;
                    audioSource.Play();
                }
            }
        }

        #endregion

        #region Event Handlers

        private void _trackable_OnTrackingChanged(object sender, TrackingChangedEventHandlerArgs args)
        {
            OnTrackingChanged(args.NewStatus == TrackableBehaviour.Status.DETECTED || args.NewStatus == TrackableBehaviour.Status.TRACKED || args.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED);
        }

        #endregion

        #region Overriding Methods

        protected override void Awake()
        {
            Instance = FindObjectOfType<SceneControllerTurbine>();
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if(_trackable)
                _trackable.OnTrackingChanged -= _trackable_OnTrackingChanged;
        }

        #endregion

        #endregion
    }
}