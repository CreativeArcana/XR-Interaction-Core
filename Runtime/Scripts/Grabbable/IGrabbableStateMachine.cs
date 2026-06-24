using System;

namespace CreativeArcana.XRInteractionCore.Grabbable
{
    public interface IGrabbableStateMachine
    {
        event Action<GrabbableState> OnStateChange;
        GrabbableState CurrentState { get; }
    }
}
