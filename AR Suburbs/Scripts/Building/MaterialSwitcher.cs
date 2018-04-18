using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Building
{
    public class MaterialSwitcher : HighlightableElement
    {
        public List<ListWrapper> Materials;
        private int _index;
        public int Index { get { return _index; } }

        public void SwitchMaterial(int index)
        {
            GetComponentsInChildren<MeshRenderer>().InvokeAction(x => x.sharedMaterials = Materials[index].List.OfType<Material>().ToArray());
        }

        public void NextMaterials()
        {
            _index++;
            if (_index >= Materials.Count)
                _index = 0;
            SwitchMaterial(_index);
        }
    }
}