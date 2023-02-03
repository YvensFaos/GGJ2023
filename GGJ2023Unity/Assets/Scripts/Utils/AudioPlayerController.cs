using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayerController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> audioClips;
        private Dictionary<string, AudioClip> _internalDictionary;

        private void Awake()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            _internalDictionary = new Dictionary<string, AudioClip>();
            audioClips.ForEach(audioClip =>
            {
                _internalDictionary.Add(audioClip.name, audioClip);
            });
        }

        public void PlayAudio(string audioClipName)
        {
            var clip = _internalDictionary[audioClipName];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError($"Cannot find audio clip with name {audioClipName}.");
            }
        }
    }
}
