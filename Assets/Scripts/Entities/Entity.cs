using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using TinyTools.ScriptableSounds;
using UnityEngine;

public abstract class Entity : SerializedMonoBehaviour
{
    [PropertySpace]
    [Title("Components")]
    [OdinSerialize] public SpriteRenderer SpriteRenderer { get; private set; }
    [OdinSerialize] public GameObject DeathParticlePrefab { get; private set; }

    [OdinSerialize] public GameObject StunParticlePrefab { get; private set; }
    [OdinSerialize] public ScriptableSound HurtSound { get; private set; }

    [PropertySpace]
    [Title("Settings")]
    [OdinSerialize] public string Name { get; private set; }
    [OdinSerialize] public int BaseHealth { get; private set; }
    [MultiLineProperty(3)]
    [OdinSerialize] public string Description { get; private set; } = string.Empty;

    [PropertySpace]
    [Title("Animation")]
    [OdinSerialize] public EntityTween MoveTween { get; private set; } = new BasicEntityTween();
    [OdinSerialize] public EntityTween HurtTween { get; private set; } = new HurtEntityTween();

    [PropertySpace]
    [Title("Abilities")]
    [OdinSerialize] public List<Ability> Abilities { get; private set; } = new List<Ability>();

    public int CurrentHealth { get; private set; }
    public Vector2Int Position { get; private set; }

    public bool CanMove { get; set; } = true;
    public bool IsDead { get; private set; } = false;
    public bool IsImmune => _immuneTurnsRemaining > 0;
    public bool IsStunned => _stunTurnsRemaining > 0;

    public event Action Destroyed;

    private int _immuneTurnsRemaining;
    private int _stunTurnsRemaining;

    protected virtual void Awake()
    {
        Position = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        CurrentHealth = BaseHealth;

        foreach (Ability ability in Abilities)
        {
            ability.Initialize(this);
        }
    }

    protected virtual void OnEnable()
    {
        EntityManager.Register(this);
    }

    protected virtual void OnDisable()
    {
        EntityManager.Unregister(this);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    protected void Flip(int x)
    {
        if (x == 0)
        {
            return;
        }

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(x) * 1;
        transform.localScale = scale;
    }

    public void Turn()
    {
        foreach (Ability ability in Abilities)
        {
            ability.Turn();
        }

        if (_immuneTurnsRemaining > 0)
        {
            _immuneTurnsRemaining--;
        }

        if (_stunTurnsRemaining > 0)
        {
            _stunTurnsRemaining--;
        }
    }

    public void Move(Vector2Int movement)
    {
        if (!CanMove || IsStunned || movement == Vector2Int.zero)
        {
            return;
        }

        Position += movement;
        Flip(movement.x);

        MoveTween.Animate(this, Position, TurnManager.TurnInterval);
    }

    public virtual void AddImmune(int turns)
    {
        _immuneTurnsRemaining += turns;
    }

    public virtual void AddStun(int turns)
    {
        _stunTurnsRemaining += turns;
        var particles = GameObject.Instantiate(StunParticlePrefab, transform);
        particles.GetComponent<ParticleSystem>().startLifetime = TurnManager.TurnInterval * turns;
    }

    public virtual void Damage(int damage)
    {
        if (IsDead || IsImmune || IsStunned)
        {
            return;
        }

        CurrentHealth -= damage;

        HurtTween.Animate(this, Vector2Int.zero, 0.2f);
        HurtSound?.Play();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int heal)
    {
        if (IsDead)
        {
            return;
        }

        CurrentHealth = Mathf.Clamp(CurrentHealth + heal, 0, BaseHealth);

        if (CurrentHealth > BaseHealth)
        {
            CurrentHealth = BaseHealth;
        }
    }

    public virtual void Die()
    {
        IsDead = true;

        if (DeathParticlePrefab != null)
        {
            Instantiate(DeathParticlePrefab, transform.position, Quaternion.identity);
        }
    }
}
