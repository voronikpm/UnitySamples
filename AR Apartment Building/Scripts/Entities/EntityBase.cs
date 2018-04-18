#region Using Directives

using System.ComponentModel;
using JetBrains.Annotations;

#endregion

namespace Assets.Scripts.Entities
{
    public class EntityBase : INotifyPropertyChanged
    {
        #region Methods

        #region Virtual Methods

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #endregion

        #region Interface Implementations

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion
    }
}