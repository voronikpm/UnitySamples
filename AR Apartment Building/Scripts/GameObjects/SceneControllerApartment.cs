#region Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.GameObjects.Building;
using Assets.Scripts.Helpers;
using Assets.Scripts.ViewModels;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Assets.Scripts.UI;
using UnityEngine.SceneManagement;

#endregion

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(AudioSource))]
    public class SceneControllerApartment : SceneControllerBase
    {
        #region Fields

        #region  Private Fields

        private AudioSource _audioSource;

        [SerializeField]
        private AudioMixer _masterMixer;

        [SerializeField]
        private AudioClip _messageReceivedClip;

        [SerializeField]
        private AudioClip _messageSentClip;

        private Furniture _selectedFurniture;

        #endregion

        #region  Public Fields

        public List<ScrollRect> Joysticks;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public bool IsMuted
        {
            get
            {
                float volume;
                _masterMixer.GetFloat("Volume", out volume);
                return volume < 0;
            }
        }

        public Furniture SelectedFurniture
        {
            get { return _selectedFurniture; }
            set
            {
                _selectedFurniture = value;
                FurnitureViewModel.Instance.Model = value;
            }
        }

        #endregion

        #region Static Properties

        public static SceneControllerApartment Instance { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Mute(bool isMuted)
        {
            _masterMixer.SetFloat("Volume", isMuted ? -80 : 0);
        }

        public void PlayChatSound(MessageSender sender)
        {
            _audioSource.PlayOneShot(sender == MessageSender.Agent ? _messageReceivedClip : _messageSentClip);
        }

        private void Start()
        {
            Joysticks.ForEach(x => x.enabled = !ApartmentViewModel.Instance.IsTutorialShown);
            _audioSource = GetComponent<AudioSource>();
            //ApartmentViewModel.Instance.RefreshMute();
            ApartmentViewModel.Instance.IsMuted = IsMuted;
            OverlayViewModel.Instance.IsMuted = IsMuted;
        }

        //TODO Consider moving to SceneControllerCommon or to the separate controller altogether
        private void Update()
        {
            if(Math.Abs(CustomInput.Axes["Move X"]) > float.Epsilon || Math.Abs(CustomInput.Axes["Move Y"]) > float.Epsilon || Math.Abs(CustomInput.Axes["Look X"]) > float.Epsilon || Math.Abs(CustomInput.Axes["Look Y"]) > float.Epsilon)
                return;
            Input.simulateMouseWithTouches = false;
            var isTouch = false;
            var touchPos = Vector2.zero;
            if(Application.isMobilePlatform)
            {
                if(Input.touches.Any())
                {
                    isTouch = Input.GetTouch(0).phase == TouchPhase.Began;
                    touchPos = Input.touches[0].position;
                }
            }
            else
            {
                isTouch = Input.GetMouseButtonDown(0);
                touchPos = Input.mousePosition;
            }
            if (ApartmentViewModel.Instance.IsFurnitureSelected)
                return;
            if(isTouch)
            {
                var ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit raycastHit;
                if(Physics.Raycast(ray, out raycastHit))
                {
                    var hit = raycastHit.transform.GetComponent<TouchableObject>();
                    if(hit)
                        hit.TouchAction();
                }
            }
        }

        public void LoadScene(string scene)
        {
            //StartCoroutine(LoadSceneCoroutine(scene));
            SceneManager.LoadScene(scene);
        }

//        private IEnumerator LoadSceneCoroutine(string scene)
//        {
//            yield return new WaitForEndOfFrame();
//#if UNITY_IPHONE
//            Handheld.SetActivityIndicatorStyle(iOS.ActivityIndicatorStyle.Gray);
//#elif UNITY_ANDROID
//            Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
//#elif UNITY_TIZEN
//            Handheld.SetActivityIndicatorStyle(TizenActivityIndicatorStyle.Small);
//#endif
//            Handheld.StartActivityIndicator();
//            SceneManager.LoadScene(scene);
//        }

        #endregion

        #region Overriding Methods

        protected override void Awake()
        {
            base.Awake();
            Instance = FindObjectOfType<SceneControllerApartment>();
            FindObjectsOfType<Camera>().InvokeAction(x => x.allowMSAA = false);
            if(!(Noesis.GUI.SoftwareKeyboard is SelectiveKeyboard))
                Noesis.GUI.SoftwareKeyboard = new SelectiveKeyboard();
        }

        #endregion

        #endregion
    }
}