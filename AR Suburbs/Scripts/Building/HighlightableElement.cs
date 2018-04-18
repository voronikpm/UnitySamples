using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Building
{
    public class HighlightableElement : MonoBehaviour
    {
        public List<VisibleProperty> Properties;
        private bool _isHighlighted;

        [SerializeField]
        private MeshRenderer _highlightedMeshRenderer;

        [SerializeField]
        private float _outlineWidth = 0.3f;

        [SerializeField]
        private Color _outlineColor = new Color(0,0.79f,1,1);

        public virtual bool IsHighlighted
        {
            get { return _isHighlighted; }
            set
            {
                _isHighlighted = value;
                if (_highlightedMeshRenderer)
                    _highlightedMeshRenderer.material.SetFloat("_Outline", value ? _outlineWidth : 0);
                MainSceneController.HighlightedObject = value ? GetComponent<HighlightableElement>() : null;
            }
        }

        protected virtual void Awake()
        {
            if(!_highlightedMeshRenderer)
                _highlightedMeshRenderer = GetComponentInChildren<MeshRenderer>(true);
            _highlightedMeshRenderer.material.SetColor("_OutlineColor",_outlineColor);
        }
    }
}