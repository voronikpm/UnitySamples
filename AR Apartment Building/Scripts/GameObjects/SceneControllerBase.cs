namespace Assets.Scripts.GameObjects
{
    public abstract class SceneControllerBase : GameObjectBase
    {
        #region Methods

        #region Virtual Methods

        protected virtual void Awake()
        {
            PostLoad();
        }

        protected virtual void PostLoad()
        {
        }

        #endregion

        #endregion
    }
}