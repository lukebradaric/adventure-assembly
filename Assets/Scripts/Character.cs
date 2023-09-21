using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    new public string name;

    public int baseHealth;

    public Sprite sprite;

    [SerializeReference]public List<Ability> abilties = new List<Ability>();
}
