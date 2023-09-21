using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    new public string name;

    public int baseHealth;

    public Sprite sprite;

    [SerializeField] private List<Class> classes = new List<Class>();

    [SerializeReference]public List<Ability> abilties = new List<Ability>();
}
