using System;
using Assets.Scripts.Enums;
using Assets.Scripts.ViewModels;

namespace Assets.Scripts.GameObjects
{
    public class TurbineElementGroup : TouchableObject
    {
        public TurbineElement Element;

        public override Action TouchAction => () => TurbineViewModel.Instance.SelectedElement = Element;
    }
}