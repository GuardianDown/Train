using UnityEngine;

namespace Train.Horn
{
    public class HornSound : IHornSound
    {
        private readonly AudioSource _audioSourcePrefab;

        private AudioSource _audioSource;

        public HornSound(AudioSource audioSourcePrefab) => _audioSourcePrefab = audioSourcePrefab;

        public void PlaySound()
        {
            if (_audioSource == null)
                _audioSource = Object.Instantiate(_audioSourcePrefab);

            if (_audioSource.isPlaying)
                _audioSource.Stop();

            _audioSource.Play();
        }
    }
}
