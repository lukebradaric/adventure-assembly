using AdventureAssembly.Core;
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

        private void OnEnable()
        {
            GoldManager.CurrentGold.ValueChanged += OnCurrentGoldChanged;
        }

        private void OnDisable()
        {
            GoldManager.CurrentGold.ValueChanged -= OnCurrentGoldChanged;
        }

        private void OnCurrentGoldChanged(int newGold)
        {
            _goldText.text = newGold.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutTransform);
        }
    }
}