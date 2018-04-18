#if UNITY_5_3_OR_NEWER
#define NOESIS
#endif
using System.Collections.Generic;
#if NOESIS
using Noesis;
using UnityEngine;
#else
using System;
#endif
using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.GameObjects;
using GUI = Noesis.GUI;

namespace Assets.Scripts.ViewModels
{
    public abstract class ViewModelBase : EntityBase
    {
        public ViewModelBase()
        {
            SceneType = SceneType.None;
        }

        public virtual SceneType SceneType { get; private set; }
        

        public virtual Language SelectedLanguage
        {
            get { return SceneControllerCommon.Instance.SelectedLanguage; }
            set
            {
                if(SceneControllerCommon.Instance.SelectedLanguage == value || value == null)
                    return;
                SceneControllerCommon.Instance.SelectedLanguage = value;
                for(int i = 0; i < SceneControllerCommon.Instance.Languages.Count; i++)
                    SceneControllerCommon.Instance.Languages[i].Name = SceneControllerCommon.Instance.SelectedLanguage[$"LangName{i}"];
                OnPropertyChanged();
            }
        }

        public  virtual List<Language> Languages
        {
            get { return SceneControllerCommon.Instance.Languages; }
            set
            {
                if(SceneControllerCommon.Instance.Languages == value)
                    return;
                SceneControllerCommon.Instance.Languages = value;
                OnPropertyChanged();
            }
        }

        public virtual void RefreshLanguage()
        {
            OnPropertyChanged(nameof(SelectedLanguage));
        }
    }
}