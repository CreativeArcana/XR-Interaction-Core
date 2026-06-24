using CreativeArcana.XRInteractionCore.Grabbable.Model;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CreativeArcana.XRInteractionCore.Grabbable.View
{
    public class ChangeImageColor : GrabbableStateChangeBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GrabbableStateColor _stateColor;
        [SerializeField] private float _duration = 0.15f;

        private Tween _colorTween;
        private Color _currentColor;

        protected override void Reset()
        {
            base.Reset();
            _image = GetComponent<Image>();
        }

        protected override void OnDisable()
        {
            _colorTween?.Kill();
            _colorTween = null;
            base.OnDisable();
        }

        protected override void HandleStateChange(GrabbableState newState)
        {
            if (_image == null || _stateColor == null)
            {
                return;
            }

            _colorTween?.Kill();
            _currentColor = _image.color;
            _colorTween = DOTween.To(() => _currentColor, SetColor, _stateColor.Get(newState), _duration);
        }

        private void SetColor(Color color)
        {
            _currentColor = color;
            _image.color = color;
        }
    }
}
