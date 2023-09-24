using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectionItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Space]
    [Header("Components")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private RectTransform _classTransformParent;
    [SerializeField] private CharacterClassItem _classInterfacePrefab;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _hoverScale = 1.3f;
    [SerializeField] private float _hoverTweenDuration = 0.2f;

    private Character _characterPrefab;

    public bool IsInteractable { get; set; } = true;

    public void SetCharacterPrefab(Character characterPrefab)
    {
        _characterPrefab = characterPrefab;

        _backgroundImage.color = characterPrefab.Color;
        _characterImage.sprite = characterPrefab.SpriteRenderer.sprite;
        _nameText.text = characterPrefab.Name;
        _descriptionText.text = characterPrefab.Description;
    }

    public void OnClick()
    {
        if (!IsInteractable)
        {
            return;
        }

        CharacterManager.AddCharacter(_characterPrefab);
        CharacterSelectionManager.Instance.CloseSelection();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(_hoverScale, _hoverTweenDuration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, _hoverTweenDuration).SetUpdate(true);
    }
}
