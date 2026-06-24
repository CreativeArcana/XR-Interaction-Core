using System;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable
{
    public class GrabbableStateMachine : MonoBehaviour, IGrabbableStateMachine
    {
        [SerializeField] private MonoBehaviour _grabbableEventsSource;
        [Header("Debug")]
        [SerializeField] private bool _isDisabled;
        [SerializeField] private bool _isHovered;
        [SerializeField] private bool _isSelected;
        [SerializeField] private bool _isActivated;

        private IGrabbableEvents _grabbableEvents;

        public event Action<GrabbableState> OnStateChange;

        public GrabbableState CurrentState { get; private set; }

        private void Awake()
        {
            _grabbableEventsSource ??= FindEventsSource();
            _grabbableEvents = _grabbableEventsSource as IGrabbableEvents;

            if (_grabbableEventsSource != null && _grabbableEvents == null)
            {
                Debug.LogError($"{nameof(_grabbableEventsSource)} must implement {nameof(IGrabbableEvents)}.", this);
            }

            CurrentState = ResolveState();
        }

        private void OnEnable()
        {
            if (_grabbableEvents == null)
            {
                return;
            }

            _grabbableEvents.OnDisabled += HandleDisabled;
            _grabbableEvents.OnHovered += HandleHovered;
            _grabbableEvents.OnUnhovered += HandleUnhovered;
            _grabbableEvents.OnSelected += HandleSelected;
            _grabbableEvents.OnUnselected += HandleUnselected;
            _grabbableEvents.OnActivated += HandleActivated;
            _grabbableEvents.OnDeactivated += HandleDeactivated;
        }

        private void OnDisable()
        {
            if (_grabbableEvents == null)
            {
                return;
            }

            _grabbableEvents.OnDisabled -= HandleDisabled;
            _grabbableEvents.OnHovered -= HandleHovered;
            _grabbableEvents.OnUnhovered -= HandleUnhovered;
            _grabbableEvents.OnSelected -= HandleSelected;
            _grabbableEvents.OnUnselected -= HandleUnselected;
            _grabbableEvents.OnActivated -= HandleActivated;
            _grabbableEvents.OnDeactivated -= HandleDeactivated;
        }

        private void Reset()
        {
            _grabbableEventsSource = FindEventsSource();
        }

        private MonoBehaviour FindEventsSource()
        {
            MonoBehaviour[] behaviours = GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IGrabbableEvents)
                {
                    return behaviour;
                }
            }

            return null;
        }

        private void HandleDisabled()
        {
            _isDisabled = true;
            _isHovered = false;
            _isSelected = false;
            _isActivated = false;
            RefreshState();
        }

        private void HandleHovered()
        {
            _isDisabled = false;
            _isHovered = true;
            RefreshState();
        }

        private void HandleUnhovered()
        {
            _isHovered = false;
            RefreshState();
        }

        private void HandleSelected()
        {
            _isDisabled = false;
            _isSelected = true;
            RefreshState();
        }

        private void HandleUnselected()
        {
            _isSelected = false;
            _isActivated = false;
            RefreshState();
        }

        private void HandleActivated()
        {
            _isDisabled = false;
            _isSelected = true;
            _isActivated = true;
            RefreshState();
        }

        private void HandleDeactivated()
        {
            _isActivated = false;
            RefreshState();
        }

        private void RefreshState()
        {
            SetState(ResolveState());
        }

        private GrabbableState ResolveState()
        {
            if (_isDisabled)
            {
                return GrabbableState.Disabled;
            }

            if (_isActivated)
            {
                return GrabbableState.Activated;
            }

            if (_isSelected)
            {
                return GrabbableState.Selected;
            }

            if (_isHovered)
            {
                return GrabbableState.Hovered;
            }

            return GrabbableState.Idle;
        }

        private void SetState(GrabbableState newState)
        {
            if (CurrentState == newState)
            {
                return;
            }

            CurrentState = newState;
            OnStateChange?.Invoke(CurrentState);
        }
    }
}
