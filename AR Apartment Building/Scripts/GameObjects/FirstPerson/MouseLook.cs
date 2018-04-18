#region Using Directives

using System;
using Assets.Scripts.Helpers;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        #region Fields

        #region  Private Fields

        private Quaternion _cameraTargetRot;
        private Quaternion _characterTargetRot;

        #endregion

        #region  Public Fields

        public bool clampVerticalRotation = true;
        public float MaximumX = 90F;
        public float MinimumX = -90F;
        public bool smooth;
        public float smoothTime = 5f;
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void Init(Transform character)
        {
            _characterTargetRot = character.localRotation;
            _cameraTargetRot = Camera.main.transform.localRotation;
        }

        public void LookRotation(Transform character)
        {
            float yRot = CustomInput.Axes["Look X"] * XSensitivity;
            float xRot = CustomInput.Axes["Look Y"] * YSensitivity;
            _characterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            _cameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
            if(clampVerticalRotation)
                _cameraTargetRot = ClampRotationAroundXAxis(_cameraTargetRot);
            if(smooth)
            {
                character.localRotation = Quaternion.Slerp(character.localRotation, _characterTargetRot, smoothTime * Time.deltaTime);
                Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, _cameraTargetRot, smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = _characterTargetRot;
                Camera.main.transform.localRotation = _cameraTargetRot;
            }
        }

        private Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;
            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
            angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);
            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
            return q;
        }

        #endregion

        #endregion
    }
}