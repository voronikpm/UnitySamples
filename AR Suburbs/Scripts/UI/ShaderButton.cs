using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ShaderButton : MonoBehaviour
    {
        public Image SelectedImage;
        public Image UnselectedImage;
        public Toggle Toggle;
        private bool _isOn;
        private void Update()
        {
            if(_isOn)
            {
                if (Toggle.isOn)
                {
                    SelectedImage.gameObject.SetActive(true);
                    UnselectedImage.gameObject.SetActive(false);
                }
                else
                {
                    SelectedImage.gameObject.SetActive(false);
                    UnselectedImage.gameObject.SetActive(true);
                }

            }
        }

        public void ChangeState(bool isOn)
        {
            _isOn = isOn;
        }
    }
}
