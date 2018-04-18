#region Using Directives

using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace Assets.Scripts.UI
{
    public class SimpleTouchController : MonoBehaviour
    {
        #region Public Delegates

        public delegate void TouchDelegateEventHandler(Vector2 value);

        public delegate void TouchStateDelegateEventHandler(bool touchPresent);

        #endregion

        #region Events

        public event TouchDelegateEventHandler TouchEvent;
        public event TouchStateDelegateEventHandler TouchStateEvent;

        #endregion

        #region Fields

        #region  Private Fields

        [SerializeField]
        private string _axes;

        [SerializeField]
        private RectTransform _joystickArea;

        private Vector2 _movementVector;

        [SerializeField]
        private float _multiplier = 1;

        private bool _touchPresent;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public Vector2 GetTouchPosition
        {
            get { return _movementVector; }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void BeginDrag()
        {
            _touchPresent = true;
            if(TouchStateEvent != null)
                TouchStateEvent(_touchPresent);
        }

        public void EndDrag()
        {
            _touchPresent = false;
            _movementVector = _joystickArea.anchoredPosition = Vector2.zero;
            if(TouchStateEvent != null)
                TouchStateEvent(_touchPresent);
        }

        public void OnValueChanged(Vector2 value)
        {
            if(_touchPresent)
            {
                // convert the value between 1 0 to -1 +1
                _movementVector.x = (1 - value.x - 0.5f) * 2f;
                _movementVector.y = (1 - value.y - 0.5f) * 2f;
                _movementVector *= _multiplier;
                if(TouchEvent != null)
                    TouchEvent(_movementVector);
                CustomInput.Axes[string.Format("{0} X", _axes)] = _movementVector.x;
                CustomInput.Axes[string.Format("{0} Y", _axes)] = _movementVector.y;
            }
            else
            {
                CustomInput.Axes[string.Format("{0} X", _axes)] = 0;
                CustomInput.Axes[string.Format("{0} Y", _axes)] = 0;
            }
        }

        #endregion

        #endregion
    }
}