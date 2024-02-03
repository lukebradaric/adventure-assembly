using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    public class TooltipElement : SerializedMonoBehaviour
    {
        [OdinSerialize] public RectTransform RectTransform { get; protected set; }
        [OdinSerialize] public CanvasGroup CanvasGroup { get; protected set; }
        [OdinSerialize] public Image Image { get; protected set; }
        [OdinSerialize] public TextMeshProUGUI Text { get; protected set; }
    }
}