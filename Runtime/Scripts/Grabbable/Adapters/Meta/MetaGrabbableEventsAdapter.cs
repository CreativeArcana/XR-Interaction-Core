using System;
using CreativeArcana.XRInteractionCore.Grabbable;
using UnityEngine;

#if META_XR_SDK_AVAILABLE
using Oculus.Interaction;
#endif

namespace CreativeArcana.XRUISet.Grabbable.Adapters
{
#if META_XR_SDK_AVAILABLE
    [RequireComponent(typeof(Oculus.Interaction.Grabbable))]
#endif
    public class MetaGrabbableEventsAdapter : MonoBehaviour, IGrabbableEvents
    {
        public event Action OnDisabled;
        public event Action OnHovered;
        public event Action OnUnhovered;
        public event Action OnSelected;
        public event Action OnUnselected;
        public event Action OnActivated;
        public event Action OnDeactivated;

#if META_XR_SDK_AVAILABLE
        private Oculus.Interaction.Grabbable _grabbable;

        private void Awake()
        {
            _grabbable = GetComponent<Oculus.Interaction.Grabbable>();
        }

        private void OnEnable()
        {
            if (_grabbable == null)
            {
                return;
            }

            _grabbable.WhenPointerEventRaised += HandlePointerEventRaised;
        }

        private void OnDisable()
        {
            if (_grabbable != null)
            {
                _grabbable.WhenPointerEventRaised -= HandlePointerEventRaised;
            }

            OnDisabled?.Invoke();
        }

        private void HandlePointerEventRaised(PointerEvent pointerEvent)
        {
            switch (pointerEvent.Type)
            {
                case PointerEventType.Hover:
                    OnHovered?.Invoke();
                    break;
                case PointerEventType.Unhover:
                    OnUnhovered?.Invoke();
                    break;
                case PointerEventType.Select:
                    OnSelected?.Invoke();
                    break;
                case PointerEventType.Unselect:
                    OnUnselected?.Invoke();
                    break;
                case PointerEventType.Move:
                    break;
                case PointerEventType.Cancel:
                    OnDeactivated?.Invoke();
                    OnUnselected?.Invoke();
                    break;
            }
        }
#else
        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
#endif

        public void RaiseActivated()
        {
            OnActivated?.Invoke();
        }

        public void RaiseDeactivated()
        {
            OnDeactivated?.Invoke();
        }
    }
}
