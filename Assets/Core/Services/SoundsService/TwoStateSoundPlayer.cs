using UnityEngine;

namespace Core.Services.Sounds
{
    public class TwoStateSoundPlayer : IService
    {
        private AudioSource source;
        private AudioClip openClip;
        private AudioClip closeClip;
        public void PlayOpenSound()
        {
            if (source == null) return;
            source.PlayOneShot(openClip);
        }
        public void PlayCloseSound()
        {
            if (source == null) return;
            source.PlayOneShot(closeClip);
        }
        public TwoStateSoundPlayer(AudioSource _source, AudioClip open, AudioClip close)
        {
            if (_source == null) return;
            source = _source;
            openClip = open;
            closeClip = close;
        }
        public TwoStateSoundPlayer() { }
    }
}