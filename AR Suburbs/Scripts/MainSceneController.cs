#region Using Directives

using System;
using Assets.Scripts.AR;
using Assets.Scripts.Building;
using Assets.Scripts.Enums;
using Assets.Scripts.Inputs;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;
using Assets.Scripts.UI;

#endregion

namespace Assets.Scripts
{
    public class MainSceneController : SceneControllerBase
    {
        #region Fields

        #region Static Fields and Constants

        private static MainSceneController _instance;

        #endregion

        #region  Public Fields

        public VuforiaBehaviour ARCamera;
        //public FirstPersonController CharacterController;
        //public NavMeshSurface RoadSurface;
        //public GameObject SecondaryCameraContainer;
        //public NavMeshSurface TerrainSurface;
        //public Camera MainCamera;

            public SceneContainer SceneContainer { get; set; }

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public House SelectedHouse { get; set; }

        #endregion

        #region Static Properties
        private static HighlightableElement _highlightableElement;
        public static HighlightableElement HighlightedObject
        {
            get { return _highlightableElement; }
            set
            {
                _highlightableElement = value;
            }
        }

        public static MainSceneController Instance
        {
            get { return _instance ?? (_instance = FindObjectOfType<MainSceneController>()); }
        }
        
        public static bool IsInfoShown { get; set; }
        
        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        //public void ChangeCamera(SceneCameraType type)
        //{
        //    var log = string.Format("{0} {1}", Camera.allCameras, Camera.main);
        //    switch (type)
        //    {
        //        case SceneCameraType.ARCamera:
        //            {
        //                ARCamera.gameObject.SetActive(true);
        //                //Camera.main.transform.parent = ARCamera.transform;
        //                //Camera.main.transform.position = ARCamera.transform.position;
        //                //Camera.main.transform.rotation = ARCamera.transform.rotation;
        //                //SecondaryCameraContainer.gameObject.SetActive(false);
        //                //CharacterController.gameObject.SetActive(false);
        //                if(SceneContainer)
        //                {
        //                    SceneContainer.SecondaryCameraContainer.gameObject.SetActive(false);
        //                    SceneContainer.CharacterController.gameObject.SetActive(false);
        //                }
        //                VuforiaBehaviour.Instance.enabled = true;
        //                //TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
        //                break;
        //            }
        //        case SceneCameraType.SecondaryCamera:
        //            {
        //                //SecondaryCameraContainer.gameObject.SetActive(true);
        //                if (SceneContainer)
        //                    SceneContainer.SecondaryCameraContainer.gameObject.SetActive(true);
        //                //Camera.main.transform.parent = SecondaryCameraContainer.transform;
        //                //Camera.main.transform.position = SecondaryCameraContainer.transform.position;
        //                //Camera.main.transform.rotation = SecondaryCameraContainer.transform.rotation;
        //                ARCamera.gameObject.SetActive(false);
        //                //CharacterController.gameObject.SetActive(false);
        //                if (SceneContainer)
        //                    SceneContainer.CharacterController.gameObject.SetActive(false);
        //                VuforiaBehaviour.Instance.enabled = false;
        //                //_sync = false;
        //                //TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        //                break;
        //            }
        //        case SceneCameraType.FirstPersonCamera:
        //            {
        //                //CharacterController.gameObject.SetActive(true);
        //                if (SceneContainer)
        //                    SceneContainer.CharacterController.gameObject.SetActive(true);
        //                //Camera.main.transform.parent = CharacterController.transform;
        //                //Camera.main.transform.position = CharacterController.transform.position;
        //                //Camera.main.transform.rotation = CharacterController.transform.rotation;
        //                ARCamera.gameObject.SetActive(false);
        //                //SecondaryCameraContainer.gameObject.SetActive(false);
        //                if (SceneContainer)
        //                    SceneContainer.SecondaryCameraContainer.gameObject.SetActive(false);
        //                VuforiaBehaviour.Instance.enabled = false;
        //                //_sync = false;
        //                //TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        //                break;
        //            }
        //        default:
        //            throw new ArgumentOutOfRangeException("type", type, null);
        //    }
        //    Debug.Log(string.Format("{0} {1}", log, Camera.main));
        //}

        //public void PlacePlayer(Transform roomTransform)
        //{
        //    //CharacterController.transform.position = roomTransform.position;
        //    //CharacterController.transform.rotation = roomTransform.rotation;
        //    if(SceneContainer)
        //    {
        //        SceneContainer.CharacterController.transform.position = roomTransform.position;
        //        SceneContainer.CharacterController.transform.rotation = roomTransform.rotation;
        //    }
        //}

        //public void RebuildNavMesh()
        //{
        //    Time.timeScale = 0;
        //    TerrainSurface.RemoveData();
        //    RoadSurface.RemoveData();
        //    TerrainSurface.BuildNavMesh();
        //    RoadSurface.BuildNavMesh();
        //    Time.timeScale = 1;
        //}

        #endregion

        #region Overriding Methods
        
        protected override void Awake()
        {
            //TrackerManager.Instance.InitTracker<ObjectTracker>();
            base.Awake();
            //Input.simulateMouseWithTouches = false;
            //ChangeCamera(SceneCameraType.ARCamera);
            //CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }

        #endregion

        #endregion
    }
}