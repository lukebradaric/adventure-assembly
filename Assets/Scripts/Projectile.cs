using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Projectiles")]
public class Projectile : ScriptableObject
{
    [Space]
    [Header("Base Stats")]
    public int speed;
    public int damage;
    public bool CanExplode;
}
