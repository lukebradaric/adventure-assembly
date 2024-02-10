using System;
using TinyTools.ScriptableEvents;
using UnityEngine;
using UnityEngine.Audio;

namespace AdventureAssembly.Core
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private VoidScriptableEvent _chestOpenedScriptableEvent;
        [SerializeField] private VoidScriptableEvent _heroSelectedScriptableEvent;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioMixer _musicAudioMixer;

        private void OnEnable()
        {
            _chestOpenedScriptableEvent.VoidEvent += OnChestOpened;
            _heroSelectedScriptableEvent.VoidEvent += OnHeroSelected;
        }

        private void OnDisable()
        {
            _chestOpenedScriptableEvent.VoidEvent -= OnChestOpened;
            _heroSelectedScriptableEvent.VoidEvent -= OnHeroSelected;
        }

        private void OnChestOpened()
        {
            _musicAudioMixer.SetFloat("MusicEQ", 0f);
        }

        private void OnHeroSelected()
        {
            _musicAudioMixer.SetFloat("MusicEQ", -80f);
        }
    }
}