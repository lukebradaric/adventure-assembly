using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : SerializedScriptableObject
{
    [Space]
    [Header("Settings")]
    new public string name;
    public int baseHealth;
    public Sprite sprite;

    [PropertySpace]
    [Title("Animation")]
    [OdinSerialize] public CharacterTween MoveTween { get; private set; } = new BasicCharacterTween();

    [PropertySpace]
    [Title("Class")]
    [OdinSerialize] public List<Class> Classes { get; private set; } = new List<Class>();

    [PropertySpace]
    [Title("Ability")]
    [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();
}
