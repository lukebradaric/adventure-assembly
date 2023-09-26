using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTools.ScriptableVariables;
using TinyTools.ScriptableEvents;
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _effectsSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private FloatScriptableVariable _effectVolume;
    [SerializeField] private FloatScriptableVariable _masterVolume;
    [SerializeField] private FloatScriptableVariable _musicVolume;
    [SerializeField] private VoidScriptableEvent OnUpdate;

    private void OnEnable()
    {
        if (_masterSlider != null)
        {
            _masterSlider.value = _masterVolume.Value;
            _effectsSlider.value = _effectVolume.Value;
            _musicSlider.value = _musicVolume.Value;
            OnUpdate.Raise();
        }
    }

    public void UpdateEffectValue()
    {
        _effectVolume.Value = _effectsSlider.value;
        OnUpdate.Raise();
    }
    public void UpdateMasterValue()
    {
        _masterVolume.Value = _masterSlider.value;
        OnUpdate.Raise();
    }
    public void UpdateMusicValue()
    {
        _musicVolume.Value = _musicSlider.value;
        OnUpdate.Raise();
    }
}
