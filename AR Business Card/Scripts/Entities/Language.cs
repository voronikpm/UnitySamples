using System.Runtime.CompilerServices;
using Noesis;

namespace Assets.Scripts.Entities
{
    public class Language : EntityBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if(_name == value)
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        private ResourceDictionary _resources;

        public ResourceDictionary Resources
        {
            get { return _resources; }
            set
            {
                if(_resources == value)
                    return;
                _resources = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        [IndexerName("Item")]
        public string this[string key] => Resources[key] as string;
    }
}