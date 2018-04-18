#region Using Directives

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects
{
    public class AnimatedGameObject : GameObjectBase
    {
        #region Fields

        #region  Private Fields

        [SerializeField]
        private List<Animator> _animators;

        [SerializeField]
        private float _duration;

        [SerializeField]
        private GameObjectBase _object;

        #endregion

        #endregion

        #region Properties

        #region Overriding Properties

        public override bool IsActive
        {
            get { return base.IsActive; }
            set
            {
                base.IsActive = value;
                if (value)
                {
                    if (SceneControllerHouse.Instance.IsFirstTime)
                        StartCoroutine(AnimationCoroutine());
                    else
                        SkipAnimation();
                }
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private IEnumerator AnimationCoroutine()
        {
            _animators.ForEach(x => x.gameObject.SetActive(true));
            yield return new WaitForSeconds(_duration);
            _object.IsActive = true;
            _animators.ForEach(x => x.gameObject.SetActive(false));
            SceneControllerHouse.Instance.IsFirstTime = false;
        }

        private void SkipAnimation()
        {
            _object.IsActive = true;
            _animators.ForEach(x => x.gameObject.SetActive(false));
        }

        #endregion

        #endregion
    }
}