#region Using Directives

using System.Collections.Generic;
using System.Windows.Input;
using Assets.Scripts.Entities;
using Assets.Scripts.GameObjects.Building;

#endregion

namespace Assets.Scripts.ViewModels
{
    public class FurnitureViewModel : ViewModelBase
    {
        #region Fields

        #region Static Fields and Constants

        private static FurnitureViewModel _instance;

        #endregion

        #region  Private Fields

        private List<FurnitureMaterial> _materials;
        private Furniture _model;
        private FurnitureMaterial _selectedMaterial;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public bool IsFurnitureSelected
        {
            get { return Model != null; }
        }

        public List<FurnitureMaterial> Materials
        {
            get { return _materials; }
            set
            {
                if(Equals(value, _materials))
                    return;
                _materials = value;
                OnPropertyChanged("Materials");
                SelectedMaterial = _model != null ? _model.SelectedMaterial : null;
            }
        }

        public Furniture Model
        {
            get { return _model; }
            set
            {
                if(_model == value)
                    return;
                _model = value;
                OnPropertyChanged("Model");
                OnPropertyChanged("IsFurnitureSelected");
                Materials = _model != null ? _model.Materials : null;
            }
        }

        public FurnitureMaterial SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                if(_selectedMaterial == value)
                    return;
                _selectedMaterial = value;
                OnPropertyChanged("SelectedMaterial");
                if(Model != null)
                    Model.SelectMaterial(_selectedMaterial);
            }
        }

        #endregion

        #region Static Properties

        public static FurnitureViewModel Instance
        {
            get { return _instance ?? (_instance = new FurnitureViewModel()); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(() =>
                                        {
                                            ApartmentViewModel.Instance.IsFurnitureSelected = false;
                                            Model = null;
                                        });
            }
        }

        #endregion

        #endregion
    }
}