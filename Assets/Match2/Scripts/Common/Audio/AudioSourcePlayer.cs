using Match2.Scripts.Common.Tools;
using UnityEngine;

namespace Match2.Scripts.Common.Audio
{
    public class AudioSourcePlayer : PooledBase
    {
        private AudioSource audioSource;

        public AudioType AudioType
        {
            get;
            set;
        }
        
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void Play()
        {
            audioSource.Play();
        }
        
        public void Play(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }       
        
        public void Play(AudioClip audioClip, float volume)
        {
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
        }

        public void Reset()
        {
            audioSource.volume = 1f;
        }
    }
}