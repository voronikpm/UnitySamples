using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class MobileControlsPanel : PanelBase
    {
        public override Func<bool> VisibilityFunc
        {
            get { return () =>/* MainSceneController.IsFirstPerson*/false; }
        }
    }
}