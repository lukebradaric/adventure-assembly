using TinyTools.ScriptableEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureAssembly.Interface
{
    /// <summary>
    /// Interface for keeping track of the players gold amount.
    /// </summary>
    public class GoldInterface : MonoBehaviour
    {
        [Space]
        [Header("Components")]
        [SerializeField] private RectTransform _layoutTransform;
        [SerializeField] private TextMeshProUGUI _goldText;

        public void OnGoldCountChanged(GameEventData gameEventData)
        {
            OnGoldCountChanged((int)gameEventData.Data);
        }

        private void OnGoldCountChanged(int goldCount)
        {
            _goldText.text = goldCount.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutTransform);
        }
    }
}