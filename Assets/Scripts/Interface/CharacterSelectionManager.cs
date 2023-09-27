using DG.Tweening;
using System.Collections.Generic;
using TinyTools.Generics;
using UnityEngine;

public class CharacterSelectionManager : Singleton<CharacterSelectionManager>
{
    [Space]
    [Header("Components")]
    [SerializeField] private CanvasGroup _interfaceCanvasGroup;
    [SerializeField] private RectTransform _layoutGroupTransform;
    [SerializeField] private CharacterSelectionItem _characterSelectionPrefab;
    [SerializeField] private CharacterListScriptableVariable _characterList;

    [Space]
    [Header("Settings")]
    [SerializeField] private float _fadeDuration;

    private void OnEnable()
    {
        //CharacterManager.LeveledUp += OnLootPickedUp;
        CharacterManager.LootPickedUp += OnLootPickedUp;
    }

    private void OnDisable()
    {
        //CharacterManager.LeveledUp -= OnLootPickedUp;
        CharacterManager.LootPickedUp -= OnLootPickedUp;
    }

    private void OnLootPickedUp()
    {
        OpenSelection();
    }

    public void OpenSelection()
    {
        List<Character> characters = new List<Character>(_characterList.Value);
        for (int i = 0; i < 3; i++)
        {
            CharacterSelectionItem selectionItem = Instantiate(_characterSelectionPrefab, _layoutGroupTransform);
            selectionItem.SetCharacterPrefab(characters.RemoveRandom());
        }

        Time.timeScale = 0;
        _interfaceCanvasGroup.interactable = true;
        _interfaceCanvasGroup.DOFade(1, _fadeDuration).SetUpdate(true);
    }

    public void CloseSelection()
    {
        foreach (Transform trans in _layoutGroupTransform.transform)
        {
            //trans.GetComponent<CharacterSelectionItem>().IsInteractable = false;
            Destroy(trans.gameObject);
        }

        Time.timeScale = 1;
        _interfaceCanvasGroup.interactable = false;
        _interfaceCanvasGroup.DOFade(0, _fadeDuration).SetUpdate(true);
    }
}
