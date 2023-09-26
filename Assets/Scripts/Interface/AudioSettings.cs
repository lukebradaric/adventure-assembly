using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TinyTools.ScriptableVariables;
public class AudioSettings : MonoBehaviour
{
    [Header("Music Group")]
    public AudioMixerGroup musicGroup;
    [SerializeField] private FloatScriptableVariable _musicVolume;
    [Header("Effects Group")]
    public AudioMixerGroup effectsGroup;
    [SerializeField] private FloatScriptableVariable _effectsVolume;
    [Header("Master Group")]
    public AudioMixerGroup masterGroup;
    [SerializeField] private FloatScriptableVariable _masterVolume;
    public void UpdateAudio()
    {
        musicGroup.audioMixer.SetFloat("musicVolume", _musicVolume.Value);
        effectsGroup.audioMixer.SetFloat("effectsVolume", _effectsVolume.Value);
        masterGroup.audioMixer.SetFloat("masterVolume", _masterVolume.Value);
    }

}
