#region Using Directives

using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.AR;
using Assets.Scripts.Enums;
using Assets.Scripts.EventArgs;
using Assets.Scripts.GameObjects.Building;
using Assets.Scripts.ViewModels;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Vuforia;
using Assets.Scripts.UI;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class SceneControllerHouse : SceneControllerBase
    {
        #region Fields

        #region  Private Fields

        private BuildingObject _buildingObject;

        //[SerializeField]
        //private SimpleGameObject _building;
        private int _floor;

        //[SerializeField]
        private FloorContainer _floorContainer;

        //TODO: Remove in the merged version
        private float _framerate;

        [SerializeField]
        private AudioMixer _masterMixer;

        private BuildingState _state;
        private TrackableObject _trackable;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public int Floor
        {
            get { return _floor; }
            set
            {
                _floor = value;
                _floorContainer.Floors.ForEach(x => x.Show(value));
            }
        }

        //TODO: Remove in the merged version
        public float Framerate
        {
            get { return _framerate; }
            set
            {
                _framerate = value;
                OverlayViewModel.Instance.Framerate = value;
            }
        }

        public bool IsMuted
        {
            get
            {
                float volume;
                _masterMixer.GetFloat("Volume", out volume);
                return volume < 0;
            }
        }

        public BuildingState State
        {
            get { return _state; }
            set
            {
                _state = value;
                switch(value)
                {
                    case BuildingState.Building:
                    {
                        if(_buildingObject != null)
                            _buildingObject.MiscObjects.ForEach(x => x.IsActive = true);
                        if(_floorContainer != null)
                            _floorContainer.Floors.ForEach(x => x.Show(true));
                        break;
                    }
                    case BuildingState.Floor:
                    {
                        if(_buildingObject != null)
                            _buildingObject.MiscObjects.ForEach(x => x.IsActive = false);
                        Floor = 0;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
        }

        #endregion

        #region Static Properties

        public static SceneControllerHouse Instance { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void ClearController()
        {
            GetComponent<UDTEventHandler>().OnTargetBuilt -= UDTEventHandler_OnTargetBuilt;
            GetComponent<UDTEventHandler>().ClearTarget();
            //Camera.main.GetComponent<VuforiaBehaviour>().enabled = false;
            //GC.Collect();
        }

        //TODO Remove in the merged version
        public void LoadApartment()
        {
            //StartCoroutine(LoadSceneCoroutine("SceneApartment"));
            SceneManager.LoadScene("SceneApartment");
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

        public void Mute(bool isMuted)
        {
            _masterMixer.SetFloat("Volume", isMuted ? -80 : 0);
        }

        public void Zoom(Touch t1, Touch t2)
        {
            if(t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
            {
                var t1p = t1.position - t1.deltaPosition;
                var t2p = t2.position - t2.deltaPosition;
                float delta = (t1.position - t2.position).magnitude - (t1p - t2p).magnitude;
                delta /= 100;
                if(delta <= -1)
                    delta = -0.9f;
                _buildingObject.transform.parent.transform.localScale *= 1 + delta;
            }
        }

        //TODO: Remove in the merged version
        private IEnumerator FramerateCoroutine()
        {
            while(true)
            {
                int lastFrameCount = Time.frameCount;
                float lastTime = Time.realtimeSinceStartup;
                yield return new WaitForSecondsRealtime(0.5f);
                float timeSpan = Time.realtimeSinceStartup - lastTime;
                int frameCount = Time.frameCount - lastFrameCount;
                Framerate = frameCount / timeSpan;
            }
        }

        private void OnDestroy()
        {
            _trackable = null;
            _buildingObject = null;
            _floorContainer = null;
            //GC.Collect();
        }

        private void ShowSuggestion(bool value)
        {
            OverlayViewModel.Instance.IsSuggestionShown = value;
        }

        private void Start()
        {
            OverlayViewModel.Instance.IsMuted = IsMuted;
            ApartmentViewModel.Instance.IsMuted = IsMuted;
            //Camera.main.GetComponent<VuforiaBehaviour>().enabled = true;
        }

        //TODO Consider moving to SceneControllerCommon or to the separate controller altogether
        private void Update()
        {

            Application.targetFrameRate = VuforiaRenderer.Instance.GetRecommendedFps(VuforiaRenderer.FpsHint.FAST);

            Input.simulateMouseWithTouches = false;
            var isTouch = false;
            var touchPos = Vector2.zero;
            var isMultitouch = false;
            if(Application.isMobilePlatform)
            {
                if(Input.touches.Any())
                {
                    isTouch = Input.GetTouch(0).phase == TouchPhase.Began;
                    touchPos = Input.touches[0].position;
                    isMultitouch = Input.touchCount == 2;
                }
            }
            else
            {
                isTouch = Input.GetMouseButtonDown(0);
                touchPos = Input.mousePosition;
            }
            if(isTouch && !isMultitouch)
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
            else if(isMultitouch)
            {
                Zoom(Input.touches[0], Input.touches[1]);
            }
        }

        #endregion

        #region Event Handlers

        //TODO move to SceneControllerCommon in the merged version
        private void Trackable_OnTrackingChanged(object sender, TrackingChangedEventHandlerArgs args)
        {
            ShowSuggestion(args.NewStatus != TrackableBehaviour.Status.DETECTED && args.NewStatus != TrackableBehaviour.Status.TRACKED && args.NewStatus != TrackableBehaviour.Status.EXTENDED_TRACKED);
        }

        private void UDTEventHandler_OnTargetBuilt(object sender, TargetBuiltEventArgs args)
        {
            if(_trackable)
                _trackable.OnTrackingChanged -= Trackable_OnTrackingChanged;
            _trackable = args.TrackableObject;
            _trackable.OnTrackingChanged += Trackable_OnTrackingChanged;
            _buildingObject = _trackable.GetComponentInChildren<BuildingObject>(true);
            _floorContainer = _buildingObject.GetComponentInChildren<FloorContainer>(true);
            HouseViewModel.Instance.MaxFloor = _floorContainer.Floors.Count - 1;
        }

        #endregion

        #region Overriding Methods

        protected override void Awake()
        {
            IsFirstTime = true;
            Instance = this;
            GetComponent<UDTEventHandler>().OnTargetBuilt += UDTEventHandler_OnTargetBuilt;
            base.Awake();
            //TODO: Remove in the merged version
            StartCoroutine(FramerateCoroutine());
            if (!(Noesis.GUI.SoftwareKeyboard is SelectiveKeyboard))
                Noesis.GUI.SoftwareKeyboard = new SelectiveKeyboard();
        }

        public bool IsFirstTime { get; set; }

        protected override void PostLoad()
        {
            base.PostLoad();
            HouseViewModel.Instance.State = BuildingState.Building;
        }

        #endregion

        #endregion
    }
}