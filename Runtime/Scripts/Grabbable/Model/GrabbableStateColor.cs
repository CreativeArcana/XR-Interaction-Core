using System;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.Model
{
    [CreateAssetMenu(menuName = "CreativeArcana/XRInteractionCore/Grabbable/StateColor", fileName = "GrabbableStateColor")]
    public class GrabbableStateColor : ScriptableObject
    {
        [SerializeField] private StateColor[] _colors;
        [SerializeField] private Color _defaultColor = Color.white;

        public Color Get(GrabbableState state)
        {
            foreach (StateColor stateColor in _colors)
            {
                if (stateColor.State == state)
                {
                    return stateColor.Color;
                }
            }

            return _defaultColor;
        }

        [Serializable]
        private struct StateColor
        {
            public GrabbableState State;
            public Color Color;
        }
    }
}
