using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTools.ScriptableSounds;
public class ContactDamage : MonoBehaviour
{
    [Space]
    [Header("Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private int _stunDuration;
    [Space]
    [Header("Components")]
    [SerializeField] private GameObject _damageParticles;
    [SerializeField] private ScriptableSound _destroySound;
    [SerializeField] private Collider2D _collider;

    public IEnumerator EnableCollider(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _collider.enabled = true;
    }
    public IEnumerator DisableCollider(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _collider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colliding with something");
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            OnEnemyCollision(enemy);
        }
    }
    protected virtual void OnEnemyCollision(Enemy enemy)
    {
        enemy.Damage(_damage);
        enemy.AddStun(_stunDuration);

        if (_damageParticles != null)
        {
            Instantiate(_damageParticles, transform.position, Quaternion.identity);
        }

        _destroySound?.Play();
    }
}
