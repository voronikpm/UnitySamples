#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    public class Floor : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        [SerializeField]
        private GameObject _exterior;

        [SerializeField]
        private GameObject _interior;

        #endregion

        #region  Public Fields

        public int Number;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Show(bool showExterior, bool showInterior = false)
        {
            _exterior.SetActive(showExterior);
            _interior.SetActive(showInterior);
            IsActive = showExterior;
            if (_enterButton)
                _enterButton.IsActive = showInterior;
        }

        [SerializeField]
        private EnterButton _enterButton;

        public void Show(int number)
        {
            Show(Number <= number, Number == number);
        }

        #endregion

        #endregion
    }
}