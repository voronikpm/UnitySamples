#region Using Directives

using Assets.Scripts.Entities;
using Assets.Scripts.Enums;

#endregion

namespace Assets.Scripts.ViewModels
{
    public abstract class ViewModelBase : EntityBase
    {
        #region Constructors

        public ViewModelBase()
        {
            SceneType = SceneType.None;
        }

        #endregion

        #region Properties

        #region Virtual Properties

        public virtual SceneType SceneType { get; private set; }

        #endregion

        #endregion
    }
}