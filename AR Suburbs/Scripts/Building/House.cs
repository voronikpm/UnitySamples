#region Using Directives

using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AR;
using Assets.Scripts.Enums;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

#endregion

namespace Assets.Scripts.Building
{
    public class House : HighlightableElement
    {
        #region Fields

        #region  Private Fields

        //private bool _isHighlighted;
        //private bool _isSelected;

        #endregion

        #region  Public Fields

        //public Transform CameraStartPosition;
        //public List<Floor> Floors;
        //public List<MiscElement> HideableElements;
        //public List<MiscElement> InteriorElements;
        //public MeshRenderer HighlightedMeshRenderer;

        [SerializeField]
        private string _scene = "HQ_ResidentialHouse/DemoScene/HouseInterior";

        public void LoadInterior()
        {
            MainSceneController.Instance.LoadScene(_scene);
        }

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        //public bool IsHighlighted
        //{
        //    get { return _isHighlighted; }
        //    set
        //    {
        //        _isHighlighted = value;
        //        if (HighlightedMeshRenderer)
        //            HighlightedMeshRenderer.material.SetColor("_OutlineColor", value ? new Color(202,255,255) : new Color(0,0,0,1));
        //        MainSceneController.HighlightedObject = value ? GetComponent<HighlightableElement>() : null;
        //    }
        //}

        //public bool IsSelected
        //{
        //    get { return _isSelected; }
        //    set
        //    {
        //        _isSelected = value;
        //        if (value)
        //        {
        //            HideableElements.ForEach(x => x.IsShown = false);
        //            ShowInterior(true);
        //            Floors.ForEach(x => x.Animate());
        //            IsHighlighted = false;
        //            Debug.Log("pre camera change");
        //            MainSceneController.Instance.ChangeCamera(SceneCameraType.SecondaryCamera);
        //            Debug.Log("post camera change");
        //            //MainSceneController.Instance.SecondaryCameraContainer.transform.position = CameraStartPosition.position;
        //            //MainSceneController.Instance.SecondaryCameraContainer.transform.rotation = CameraStartPosition.rotation;
        //            if(MainSceneController.Instance.SceneContainer)
        //            {
        //                MainSceneController.Instance.SceneContainer.SecondaryCameraContainer.transform.position = CameraStartPosition.position;
        //                MainSceneController.Instance.SceneContainer.SecondaryCameraContainer.transform.rotation = CameraStartPosition.rotation;
        //            }
        //            if(Camera.main && Camera.main.GetComponent<AnimatedElement>())
        //                Camera.main.GetComponent<AnimatedElement>().Animate();
        //            SetRoomsActive(true);
        //        }
        //        else
        //        {
        //            ShowHiddenElements();
        //            ResetFloors();
        //            InteriorElements.ForEach(x => x.IsShown = false);
        //            MainSceneController.Instance.ChangeCamera(SceneCameraType.ARCamera);
        //        }
        //        if (MainSceneController.Instance.SelectedHouse && MainSceneController.Instance.SelectedHouse.IsSelected)
        //            MainSceneController.Instance.SelectedHouse.IsSelected = false;
        //        MainSceneController.Instance.SelectedHouse = value ? this : null;
        //    }
        //}

        //[SerializeField]
        //private List<MiscElement> _environment;

        //public void HideEnvironment()
        //{
        //    _environment.ForEach(x => x.IsShown = false);
        //}

        //public void ResetFloors()
        //{
        //    Floors.ForEach(x => x.Reset());
        //    ShowHiddenElements();
        //}

        //public void ShowHiddenElements()
        //{
        //    HideableElements.ForEach(x => x.Reset());
        //}

        //public void ShowInterior(bool value)
        //{
        //    InteriorElements.ForEach(x => x.IsShown = value);
        //}

        //public void SetRoomsActive(bool isActive)
        //{
        //    GetComponentsInChildren<Room>(true).Select(x => x.gameObject).InvokeAction(x => x.SetActive(isActive));
        //}

        #endregion

        #endregion
    }
}