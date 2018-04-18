namespace Assets.Scripts.Building
{
    public class MiscElement : AnimatedElement
    {
        #region Properties

        #region Regular Properties

        public bool IsShown
        {
            get { return gameObject.activeSelf; }
            set { gameObject.SetActive(value); }
        }
        
        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void AnimatedHide()
        {
            if(!Animate())
                IsShown = false;
        }

        public void OnAnimationEnd()
        {
            IsShown = false;
        }

        public override void Reset()
        {
            IsShown = true;
            base.Reset();
        }

        #endregion

        #endregion
    }
}