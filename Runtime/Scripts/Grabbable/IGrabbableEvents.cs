using System;

namespace CreativeArcana.XRInteractionCore.Grabbable
{
    public interface IGrabbableEvents
    {
        event Action OnDisabled;
        event Action OnHovered;
        event Action OnUnhovered;
        event Action OnSelected;
        event Action OnUnselected;
        event Action OnActivated;
        event Action OnDeactivated;
    }
}
