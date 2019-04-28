using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Weapon _weapon;
    private float health = 100;

    public void Initialize(Weapon stats)
    {
        _weapon = stats;
        Invoke("Die", _weapon.Stats.Lifetime);
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
            if (Random.value > _weapon.Stats.PierceRate)
            {
                health -= 100;
            }

            if (health <= 0)
                Die();

            unit.TakeDamage(_weapon.Stats.Damage);
        }
    }
}
