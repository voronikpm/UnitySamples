#region Using Directives

using System.ComponentModel;
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.PostProcessing;

#endregion

namespace Assets.Scripts.GameObjects
{
    public abstract class SceneControllerBase : GameObjectBase
    {
        #region Fields

        #region  Protected Fields

        protected Settings _Settings;

        #endregion

        #endregion

        #region Properties

        #region Virtual Properties

        public virtual SceneType ControllerSceneType => SceneType.None;

        public virtual bool IsMuted { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Event Handlers

        protected virtual void Settings_OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(_Settings.IsMuted):
                {
                    IsMuted = _Settings.IsMuted;
                    break;
                }
                //case nameof(_Settings.IsAmbientOcclusionOn):
                //{
                //    var profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
                //    profile.ambientOcclusion.enabled = _Settings.IsAmbientOcclusionOn;
                //    break;
                //}
                case nameof(_Settings.IsAntiAliasingOn):
                {
                    var profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
                    profile.antialiasing.enabled = _Settings.IsAntiAliasingOn;
                    break;
                }
                case nameof(_Settings.IsLightOn):
                {
                    Camera.main.GetComponentInChildren<Light>(true).gameObject.SetActive(_Settings.IsLightOn);
                    break;
                }
                //case nameof(_Settings.SelectedLanguageIndex):
                //{
                //    SceneControllerCommon.Instance.SelectedLanguage = SceneControllerCommon.Instance.Languages[_Settings.SelectedLanguageIndex];
                //    break;
                //}
            }
        }

        protected virtual void ApplySettings()
        {
            var profile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
            //profile.ambientOcclusion.enabled = _Settings.IsAmbientOcclusionOn;
            profile.antialiasing.enabled = _Settings.IsAntiAliasingOn;
            Camera.main.GetComponentInChildren<Light>(true).gameObject.SetActive(_Settings.IsLightOn);
            SceneControllerCommon.Instance.SelectedLanguage = SceneControllerCommon.Instance.Languages[_Settings.SelectedLanguageIndex];
            IsMuted = _Settings.IsMuted;
        }
        
        #endregion

        #region Virtual Methods

        protected virtual void Awake()
        {
            PostLoad();
        }

        protected virtual void OnDestroy()
        {
            UnhookEventListeners();
        }

        public virtual void UnhookEventListeners()
        {
            if (_Settings != null)
                _Settings.PropertyChanged -= Settings_OnPropertyChanged;
        }

        protected virtual void PostLoad()
        {
            _Settings = SceneControllerCommon.Instance.SettingsDictionary[ControllerSceneType];
            ApplySettings();
            if(_Settings != null)
                _Settings.PropertyChanged += Settings_OnPropertyChanged;
        }

        #endregion

        #endregion
    }
}