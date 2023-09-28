using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterClassItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Space]
    [Header("Components")]
    public Image image;
    public TextMeshProUGUI text;
    public Image descriptionImage;
    public TextMeshProUGUI descriptionText;
    public CanvasGroup description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.DOFade(1, 0.2f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.DOFade(0, 0.2f).SetUpdate(true);
    }

    public void SetClass(Class cl)
    {
        image.color = cl.color;
        text.text = cl.name;
        descriptionImage.color = cl.color;
        descriptionText.text = cl.description;
    }
}
