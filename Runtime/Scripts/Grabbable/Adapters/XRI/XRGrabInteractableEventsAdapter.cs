using System;
using CreativeArcana.XRInteractionCore.Grabbable;
using UnityEngine;

#if UNITY_XR_INTERACTION_TOOLKIT_AVAILABLE
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#endif

namespace CreativeArcana.XRUISet.Grabbable.Adapters
{
#if UNITY_XR_INTERACTION_TOOLKIT_AVAILABLE
    [RequireComponent(typeof(XRGrabInteractable))]
#endif
    public class XRGrabInteractableEventsAdapter : MonoBehaviour, IGrabbableEvents
    {
        public event Action OnDisabled;
        public event Action OnHovered;
        public event Action OnUnhovered;
        public event Action OnSelected;
        public event Action OnUnselected;
        public event Action OnActivated;
        public event Action OnDeactivated;

#if UNITY_XR_INTERACTION_TOOLKIT_AVAILABLE
        private XRGrabInteractable _grabInteractable;

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
        }

        private void OnEnable()
        {
            if (_grabInteractable == null)
            {
                return;
            }

            _grabInteractable.hoverEntered.AddListener(HandleHoverEntered);
            _grabInteractable.hoverExited.AddListener(HandleHoverExited);
            _grabInteractable.selectEntered.AddListener(HandleSelectEntered);
            _grabInteractable.selectExited.AddListener(HandleSelectExited);
            _grabInteractable.activated.AddListener(HandleActivated);
            _grabInteractable.deactivated.AddListener(HandleDeactivated);
        }

        private void OnDisable()
        {
            if (_grabInteractable != null)
            {
                _grabInteractable.hoverEntered.RemoveListener(HandleHoverEntered);
                _grabInteractable.hoverExited.RemoveListener(HandleHoverExited);
                _grabInteractable.selectEntered.RemoveListener(HandleSelectEntered);
                _grabInteractable.selectExited.RemoveListener(HandleSelectExited);
                _grabInteractable.activated.RemoveListener(HandleActivated);
                _grabInteractable.deactivated.RemoveListener(HandleDeactivated);
            }

            OnDisabled?.Invoke();
        }

        private void HandleHoverEntered(HoverEnterEventArgs eventArgs)
        {
            OnHovered?.Invoke();
        }

        private void HandleHoverExited(HoverExitEventArgs eventArgs)
        {
            OnUnhovered?.Invoke();
        }

        private void HandleSelectEntered(SelectEnterEventArgs eventArgs)
        {
            OnSelected?.Invoke();
        }

        private void HandleSelectExited(SelectExitEventArgs eventArgs)
        {
            OnUnselected?.Invoke();
        }

        private void HandleActivated(ActivateEventArgs eventArgs)
        {
            OnActivated?.Invoke();
        }

        private void HandleDeactivated(DeactivateEventArgs eventArgs)
        {
            OnDeactivated?.Invoke();
        }
#else
        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
#endif
    }
}
