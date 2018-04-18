#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Assets.Scripts.UI
{
    public class StatsPanel : PanelBase
    {
        #region Fields

        #region  Private Fields

        private Vector3 _prev;
        private List<VisibleProperty> _properties;

        #endregion

        #region  Public Fields

        public PropertyPanel PropertyPanelTemplate;
        public VerticalLayoutGroup PropertyContainer;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public List<VisibleProperty> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                if(_properties != null)
                    _properties.ForEach(x =>
                                        {
                                            var panel = Instantiate(PropertyPanelTemplate, PropertyContainer.transform);
                                            panel.Property = x;
                                        });
            }
        }

        #endregion

        #region Overriding Properties

        public override Func<bool> VisibilityFunc
        {
            get { return () => MainSceneController.HighlightedObject/* && !MainSceneController.IsFirstPerson*/; }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void OnDisable()
        {
            Properties = null;
            GetComponentsInChildren<PropertyPanel>().Select(x => x.gameObject).InvokeAction(Destroy);
            _prev = Vector3.zero;
        }

        private void OnEnable()
        {
            Properties = MainSceneController.HighlightedObject.Properties;
            //GetComponent<RectTransform>().position = MainSceneController.TouchPos;
            GetComponent<RectTransform>().position = InputController.TouchPos;
        }

        private void Update()
        {
            if(MainSceneController.HighlightedObject)
            {
                var screenPoint = Camera.main.WorldToViewportPoint(MainSceneController.HighlightedObject.transform.position);
                bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                if(!onScreen)
                {
                    MainSceneController.HighlightedObject = null;
                }
                else
                {
                    if(_prev != Vector3.zero)
                    {
                        var dif = screenPoint - _prev;
                        var mul = MainCanvas.Instance.GetComponent<RectTransform>().sizeDelta;
                        GetComponent<RectTransform>().position += new Vector3(dif.x * mul.x, dif.y * mul.y);
                    }
                    _prev = screenPoint;
                }
            }
        }

        #endregion

        #endregion
    }
}