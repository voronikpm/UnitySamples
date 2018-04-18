#region Using Directives

using System;
using System.Collections.Generic;
using Assets.Scripts.Building;
using UnityEngine;

#endregion

namespace Assets.Scripts.UI
{
    public class ButtonPanel : PanelBase
    {
        #region Fields

        #region  Private Fields

        private Vector3 _prev;

        public List<ShaderButton> ShaderButtons;

        #endregion

        #endregion

        #region Properties

        #region Overriding Properties

        public override Func<bool> VisibilityFunc
        {
            get { return () => /*MainSceneController.IsFirstPerson && MainSceneController.HighlightedObject*/ false; }
        }

        private static ButtonPanel _instance;
        public static ButtonPanel Instance { get { return _instance ?? (_instance = FindObjectOfType<Canvas>().GetComponentInChildren<ButtonPanel>(true)); } }
        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void ChangeMaterial(int material = 0)
        {
            if (MainSceneController.HighlightedObject)
            {
                var mat = MainSceneController.HighlightedObject.GetComponent<MaterialSwitcher>();
                if (mat)
                    mat.SwitchMaterial(material);
            }
        }

        private void OnDisable()
        {
            _prev = Vector3.zero;
            ClearSelection();
        }

        public void ClearSelection()
        {

            for (var i = 0; i < ShaderButtons.Count; i++)
            {
                ShaderButtons[i].ChangeState(false);
                ShaderButtons[i].Toggle.isOn = false;
            }
        }

        public void UpdateSelection()
        {
            if(MainSceneController.HighlightedObject)
            {
                var index = MainSceneController.HighlightedObject.GetComponent<MaterialSwitcher>().Index;
                for (var i = 0; i < ShaderButtons.Count; i++)
                {
                    ShaderButtons[i].Toggle.isOn = index == i;
                    ShaderButtons[i].ChangeState(true);
                }
            }
        }

        private void OnEnable()
        {
            //GetComponent<RectTransform>().position = MainSceneController.TouchPos;
            GetComponent<RectTransform>().position = InputController.TouchPos;
            UpdateSelection();

        }
        private void Update()
        {
            if (MainSceneController.HighlightedObject)
            {
                var screenPoint = Camera.main.WorldToViewportPoint(MainSceneController.HighlightedObject.transform.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                if (!onScreen)
                {
                    MainSceneController.HighlightedObject = null;
                }
                else
                {
                    if (_prev != Vector3.zero)
                    {
                        var dif = screenPoint - _prev;
                        var mul = MainCanvas.Instance.GetComponent<RectTransform>().sizeDelta;
                        GetComponent<RectTransform>().position += new Vector3(dif.x * mul.x, dif.y * mul.y);
                    }
                    _prev = screenPoint;
                }
            }
            else
                ClearSelection();
        }

        #endregion

        #endregion
    }
}