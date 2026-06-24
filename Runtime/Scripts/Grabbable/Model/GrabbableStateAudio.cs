#if VCONTAINER_AVAILABLE && CREATIVEARCANA_AUDIO_AVAILABLE
using System;
using CreativeArcana.Audio;
using UnityEngine;

namespace CreativeArcana.XRInteractionCore.Grabbable.Model
{
    [CreateAssetMenu(menuName = "CreativeArcana/XRInteractionCore/Grabbable/StateAudio", fileName = "GrabbableStateAudio")]
    public class GrabbableStateAudio : ScriptableObject
    {
        [SerializeField] private StateAudio[] _audioEntries;

        public bool TryGet(GrabbableState state, out AudioEntry audioEntry, out float fadeInDuration)
        {
            foreach (StateAudio stateAudio in _audioEntries)
            {
                if (stateAudio.State == state)
                {
                    audioEntry = stateAudio.AudioEntry;
                    fadeInDuration = stateAudio.FadeInDuration;
                    return audioEntry != null;
                }
            }

            audioEntry = null;
            fadeInDuration = 0f;
            return false;
        }

        [Serializable]
        private struct StateAudio
        {
            public GrabbableState State;
            public AudioEntry AudioEntry;
            public float FadeInDuration;
        }
    }
}

#endif
