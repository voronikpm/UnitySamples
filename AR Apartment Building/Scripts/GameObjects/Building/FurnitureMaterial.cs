#region Using Directives

using System;
using System.Collections.Generic;
using Assets.Scripts.Entities;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    [Serializable]
    public class FurnitureMaterial : EntityBase
    {
        #region Fields

        #region  Private Fields

        [SerializeField]
        private List<Material> _materials;

        [SerializeField]
        private string _name;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public List<Material> Material
        {
            get { return _materials; }
            set
            {
                if(Equals(value, _materials))
                    return;
                _materials = value;
                OnPropertyChanged("Material");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if(value == _name)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #endregion
    }
}