#if VCONTAINER_AVAILABLE && CREATIVEARCANA_AUDIO_AVAILABLE

using CreativeArcana.Audio;
using CreativeArcana.XRInteractionCore.Grabbable.Model;
using UnityEngine;
using VContainer;

namespace CreativeArcana.XRInteractionCore.Grabbable.View
{
    public class PlayGrabbableStateSound : GrabbableStateChangeBehaviour
    {
        [SerializeField] private GrabbableStateAudio _stateAudio;
        [SerializeField] private bool _playInitialState;

        private IAudioService _audioService;
        private bool _hasHandledFirstState;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        protected override void HandleStateChange(GrabbableState newState)
        {
            if (!_playInitialState && !_hasHandledFirstState)
            {
                _hasHandledFirstState = true;
                return;
            }

            _hasHandledFirstState = true;
            Play(newState);
        }

        private void Play(GrabbableState state)
        {
            if (_audioService == null || _stateAudio == null)
            {
                return;
            }

            if (!_stateAudio.TryGet(state, out AudioEntry audioEntry, out float fadeInDuration))
            {
                return;
            }

            _audioService.Play(audioEntry, fadeInDuration);
        }
    }
}

#else

using CreativeArcana.XRInteractionCore.Grabbable;
using CreativeArcana.XRInteractionCore.Grabbable.View;
using UnityEngine;

namespace CreativeArcana.XRUISet.Grabbable.Visuals
{
    public class PlayGrabbableStateSound : GrabbableStateChangeBehaviour
    {
        protected override void HandleStateChange(GrabbableState newState)
        {
        }

        private void Start()
        {
            Debug.LogWarning("Add VCONTAINER && CREATIVEARCANA_AUDIO Packages and Defines", this);
        }
    }
}

#endif
