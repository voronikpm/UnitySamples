using Assets.Scripts.Enums;

namespace Assets.Scripts.EventArgs
{
    public class SceneLoadedEventArgs
    {
        #region Constructors

        public SceneLoadedEventArgs()
        {
            Scene = SceneType.None;
        }

        public SceneLoadedEventArgs(SceneType scene)
        {
            Scene = scene;
        }

        #endregion

        #region Properties

        #region Regular Properties

        public SceneType Scene { get; private set; }

        #endregion

        #endregion

        #region Methods

        #region Operators

        public static implicit operator SceneLoadedEventArgs(SceneType scene)
        {
            return new SceneLoadedEventArgs(scene);
        }

        public static implicit operator SceneType(SceneLoadedEventArgs args)
        {
            return args.Scene;
        }

        #endregion

        #endregion
    }
}