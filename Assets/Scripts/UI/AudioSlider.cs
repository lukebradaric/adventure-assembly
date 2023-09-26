using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTools.ScriptableVariables;
using TinyTools.ScriptableEvents;
public class AudioSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private FloatScriptableVariable _effectVolume;
    [SerializeField] private VoidScriptableEvent OnUpdate;

    public void UpdateEffectValue()
    {
        _effectVolume.Value = _slider.value;
        OnUpdate.Raise();
    }
}
