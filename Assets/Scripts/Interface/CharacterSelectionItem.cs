using DG.Tweening;
using System.Collections.Generic;
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
    [SerializeField] private GameObject _spawnArea;

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

        foreach (Class cl in characterPrefab.Classes)
        {
            CharacterClassItem classItem = Instantiate(_classInterfacePrefab, _classTransformParent);
            classItem.SetClass(cl);
        }
    }

    public void OnClick()
    {
        if (!IsInteractable)
        {
            return;
        }
        //Instead of adding character straight from click,
        //players must pick up character dropped into in-game world
        //nah i give up 
        /*
        List<Vector2> availablePositions = new List<Vector2>();
        Vector2 size = _spawnArea.GetComponent<BoxCollider2D>().size;
        int totalCells = (int)size.x * (int)size.y;
        Vector2 startPosition = new Vector2(-size.x / 2, size.y / 2 + 0.5f);
        for(int rows = 0; rows < size.x; rows++)
        {
            for(int col = 0; col < size.y; col++)
            {
                availablePositions.Add(new Vector2(startPosition.x + (1 * rows), startPosition.y + (1 * col)));
            }
        }
        bool foundSpot = false;
        while (!foundSpot)
        {
            int index = Random.Range(0, availablePositions.Count);
            
        }
        */

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
