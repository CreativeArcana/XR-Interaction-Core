using System;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.Model
{
    [CreateAssetMenu(menuName = "CreativeArcana/XRInteractionCore/Grabbable/StateScale", fileName = "GrabbableStateScale")]
    public class GrabbableStateScale : ScriptableObject
    {
        [SerializeField] private StateScale[] _scales;
        [SerializeField] private Vector3 _defaultScale = Vector3.one;

        public Vector3 Get(GrabbableState state)
        {
            foreach (StateScale stateScale in _scales)
            {
                if (stateScale.State == state)
                {
                    return stateScale.Scale;
                }
            }

            return _defaultScale;
        }

        [Serializable]
        private struct StateScale
        {
            public GrabbableState State;
            public Vector3 Scale;
        }
    }
}
