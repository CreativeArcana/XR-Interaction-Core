using CreativeArcana.XRInteractionCore.Grabbable.Model;
using DG.Tweening;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.View
{
    public class ChangeGameObjectScale : GrabbableStateChangeBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GrabbableStateScale _stateScale;
        [SerializeField] private float _duration = 0.15f;

        private Tween _scaleTween;

        protected override void Reset()
        {
            base.Reset();
            _target = transform;
        }

        protected override void OnDisable()
        {
            _scaleTween?.Kill();
            _scaleTween = null;
            base.OnDisable();
        }

        protected override void HandleStateChange(GrabbableState newState)
        {
            if (_target == null || _stateScale == null)
            {
                return;
            }

            _scaleTween?.Kill();
            _scaleTween = _target.DOScale(_stateScale.Get(newState), _duration);
        }
    }
}
