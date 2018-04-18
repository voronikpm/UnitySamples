using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Assets.Scripts.Entities
{
    public class EntityBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}