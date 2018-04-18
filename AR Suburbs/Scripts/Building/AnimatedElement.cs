#region Using Directives

using System;
using UnityEngine;

#endregion

namespace Assets.Scripts.Building
{
    public class AnimatedElement : MonoBehaviour
    {
        #region Fields

        #region Static Fields and Constants

        public string AnimationName = "Move";
        public string ResetAnimationName = "Reset";

        #endregion

        public bool IsAnimated = true;

        #endregion

        #region Methods

        #region Virtual Methods

        public virtual bool Animate()
        {
            if (!IsAnimated)
                return false;
            var animator = GetComponent<Animator>();
            if(animator)
                animator.Play(AnimationName);
            return animator;
        }

        public virtual void Reset()
        {
            if (!IsAnimated)
                return;
            var animator = GetComponent<Animator>();
            if(animator)
                animator.Play(ResetAnimationName);
        }
        

        #endregion

        #endregion
    }
}