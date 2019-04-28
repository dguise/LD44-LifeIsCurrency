using UnityEngine;
using System.Collections;

public class CustomEnemy : Enemy
{
    [Header("Weapon stats")]
    public float damage;
    public float rateOfFire;
    public float lifetime;
    public float projectileSpeed;
    public float pierce;
    public int projectiles;
    public GameObject projectile;

    void Start()
    {
        weapon = new CustomEnemyCanon(gameObject, damage, rateOfFire, lifetime, projectileSpeed, pierce, projectiles, projectile);
    }
}
