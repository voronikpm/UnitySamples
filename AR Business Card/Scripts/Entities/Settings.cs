using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Assets.Scripts.Enums;
using JetBrains.Annotations;

namespace Assets.Scripts.Entities
{
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isMuted;

        public bool IsMuted
        {
            get { return _isMuted; }
            set
            {
                if (_isMuted == value)
                    return;
                _isMuted = value;
                OnPropertyChanged();
            }
        }

        private bool _isLightOn = true;

        public bool IsLightOn
        {
            get { return _isLightOn; }
            set
            {
                if (_isLightOn == value)
                    return;
                _isLightOn = value;
                OnPropertyChanged();
            }
        }

        //private bool _isAmbientOcclusionOn;

        //public bool IsAmbientOcclusionOn
        //{
        //    get { return _isAmbientOcclusionOn; }
        //    set
        //    {
        //        if (_isAmbientOcclusionOn == value)
        //            return;
        //        _isAmbientOcclusionOn = value;
        //        OnPropertyChanged();
        //    }
        //}

        private bool _isAntiAliasingOn;

        public bool IsAntiAliasingOn
        {
            get { return _isAntiAliasingOn; }
            set
            {
                if (_isAntiAliasingOn == value)
                    return;
                _isAntiAliasingOn = value;
                OnPropertyChanged();
            }
        }

        private int _selectedLanguageIndex;

        public int SelectedLanguageIndex
        {
            get { return _selectedLanguageIndex; }
            set
            {
                if(_selectedLanguageIndex == value)
                    return;
                _selectedLanguageIndex = value;
                OnPropertyChanged();
            }
        }
    }
}