using TinyTools.ScriptableEvents;
using UnityEngine;
using UnityEngine.Audio;

namespace AdventureAssembly.Core
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioMixer _musicAudioMixer;

        public void OnChestOpened(GameEventData gameEventData)
        {
            _musicAudioMixer.SetFloat("MusicEQ", 0f);
        }

        public void OnHeroSelected(GameEventData gameEventData)
        {
            _musicAudioMixer.SetFloat("MusicEQ", -80f);
        }
    }
}