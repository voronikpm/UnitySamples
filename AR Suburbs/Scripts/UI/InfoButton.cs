using System;
using Assets.Scripts.Building;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class InfoButton : ButtonBase
    {
        public override Func<bool> VisibilityFunc
        {
            get { return () => /*MainSceneController.IsFirstPerson*/false; }
        }

        public override Action Action
        {
            get
            {
                return () =>
                       {
                           MainSceneController.IsInfoShown = !MainSceneController.IsInfoShown;
                           if(MainSceneController.IsInfoShown)
                               GetComponent<AnimatedElement>().Animate();
                           else
                               GetComponent<AnimatedElement>().Reset();
                       };
            }
        }

        public void ChangeMaterial(int material = 0)
        {
            if(MainSceneController.HighlightedObject)
            {
                var mat = MainSceneController.HighlightedObject.GetComponent<MaterialSwitcher>();
                if(mat)
                {
                    mat.SwitchMaterial(material);
                }
            }

        }
    }
}