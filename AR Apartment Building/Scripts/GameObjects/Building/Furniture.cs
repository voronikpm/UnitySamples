#region Using Directives

using System.Collections.Generic;
using Assets.Scripts.ViewModels;
using UnityEngine;

#endregion

namespace Assets.Scripts.GameObjects.Building
{
    public class Furniture : GameObjectBase
    {
        #region Fields

        #region  Public Fields

        public Furniture ConnectedFurniture;
        public List<FurnitureMaterial> Materials;

        #endregion

        #endregion

        #region Properties

        #region Regular Properties

        public FurnitureMaterial SelectedMaterial { get; set; }

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        public void SelectMaterial(FurnitureMaterial material)
        {
            SelectedMaterial = material ?? Materials[0];
            GetComponent<MeshRenderer>().sharedMaterials = SelectedMaterial.Material.ToArray();
            if(ConnectedFurniture && ConnectedFurniture.SelectedMaterial != SelectedMaterial)
                ConnectedFurniture.SelectMaterial(material);
        }

        public void ShowDetails()
        {
            bool isSelecting = FurnitureViewModel.Instance.Model != this;
            ApartmentViewModel.Instance.IsFurnitureSelected = isSelecting;
            FurnitureViewModel.Instance.Model = isSelecting ? this : null;
        }

        //public override Action TouchAction
        //{
        //    get
        //    {
        //        return () =>
        //               {
        //                   var isSelecting = FurnitureViewModel.Instance.Model != this;
        //                   ApartmentViewModel.Instance.IsFurnitureSelected = isSelecting;
        //                   FurnitureViewModel.Instance.Model = isSelecting ? this : null;
        //               };
        //    }
        //}

        private void Awake()
        {
            SelectMaterial(Materials[0]);
        }

        #endregion

        #endregion
    }
}