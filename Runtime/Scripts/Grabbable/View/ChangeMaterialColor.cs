using CreativeArcana.XRInteractionCore.Grabbable.Model;
using DG.Tweening;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.View
{
    public class ChangeMaterialColor : GrabbableStateChangeBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GrabbableStateColor _stateColor;
        [SerializeField] private string _colorProperty = "_BaseColor";
        [SerializeField] private float _duration = 0.15f;

        private MaterialPropertyBlock _propertyBlock;
        private Tween _colorTween;
        private Color _currentColor;

        protected override void Awake()
        {
            base.Awake();
            _propertyBlock = new MaterialPropertyBlock();
        }

        protected override void Reset()
        {
            base.Reset();
            _renderer = GetComponent<Renderer>();
        }

        protected override void OnDisable()
        {
            _colorTween?.Kill();
            _colorTween = null;
            base.OnDisable();
        }

        protected override void HandleStateChange(GrabbableState newState)
        {
            if (_renderer == null || _stateColor == null)
            {
                return;
            }

            _currentColor = GetCurrentColor();
            _colorTween?.Kill();
            _colorTween = DOTween.To(() => _currentColor, SetColor, _stateColor.Get(newState), _duration);
        }

        private Color GetCurrentColor()
        {
            _renderer.GetPropertyBlock(_propertyBlock);

            if (_propertyBlock.HasColor(_colorProperty))
            {
                return _propertyBlock.GetColor(_colorProperty);
            }

            Material sharedMaterial = _renderer.sharedMaterial;
            return sharedMaterial != null && sharedMaterial.HasProperty(_colorProperty)
                ? sharedMaterial.GetColor(_colorProperty)
                : Color.white;
        }

        private void SetColor(Color color)
        {
            _currentColor = color;
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(_colorProperty, color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
