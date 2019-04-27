using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Weapon _stats;
    private float health = 100;

    public void Initialize(Weapon stats)
    {
        _stats = stats;
    }

    private void Start()
    {
        Invoke("Die", 5);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        var unit = coll.GetComponent<Unit>();
        if (unit)
        {
            health -= 100 - (100 * _stats.PierceRate);
            if (health <= 0)
                Die();

            unit.TakeDamage(_stats.Damage);
        }
    }
}
