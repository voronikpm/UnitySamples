#region Using Directives

using Assets.Scripts.Enums;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        private bool _isReverse;

        #endregion

        #endregion

        #region Methods

        #region Virtual Methods

        public virtual void OnEnd()
        {
            if(!_isReverse)
                SceneControllerTurbine.Instance.OnAnimationEnd();
        }

        public virtual void OnStart()
        {
            if(_isReverse)
                SceneControllerTurbine.Instance.OnAnimationEnd();
        }

        public virtual void Play(TurbineAnimationType type)
        {
            _isReverse = type == TurbineAnimationType.RevertCutoff || type == TurbineAnimationType.RevertMove || type == TurbineAnimationType.StopRotation;
            GetComponent<Animator>().Play(type.ToString());
        }

        #endregion

        #endregion
    }
}