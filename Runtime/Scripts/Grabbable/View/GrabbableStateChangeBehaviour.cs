using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.View
{
    public abstract class GrabbableStateChangeBehaviour : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _stateMachineSource;

        private IGrabbableStateMachine _stateMachine;

        protected virtual void Awake()
        {
            _stateMachineSource ??= FindStateMachineSource();
            _stateMachine = _stateMachineSource as IGrabbableStateMachine;

            if (_stateMachineSource != null && _stateMachine == null)
            {
                Debug.LogError($"{nameof(_stateMachineSource)} must implement {nameof(IGrabbableStateMachine)}.", this);
            }
        }

        protected virtual void OnEnable()
        {
            if (_stateMachine == null)
            {
                return;
            }

            _stateMachine.OnStateChange += HandleStateChange;
            HandleStateChange(_stateMachine.CurrentState);
        }

        protected virtual void OnDisable()
        {
            if (_stateMachine == null)
            {
                return;
            }

            _stateMachine.OnStateChange -= HandleStateChange;
        }

        protected virtual void Reset()
        {
            _stateMachineSource = FindStateMachineSource();
        }

        private MonoBehaviour FindStateMachineSource()
        {
            MonoBehaviour[] behaviours = GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour behaviour in behaviours)
            {
                if (behaviour is IGrabbableStateMachine)
                {
                    return behaviour;
                }
            }

            return null;
        }

        protected abstract void HandleStateChange(GrabbableState newState);
    }
}
