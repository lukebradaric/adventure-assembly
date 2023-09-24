using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterClassItem : MonoBehaviour
{
    [Space]
    [Header("Components")]
    public Image image;
    public TextMeshProUGUI text;

    public void SetClass(Class cl)
    {
        image.color = cl.color;
        text.text = cl.name;
    }
}
