using UnityEngine;
using System.Collections;

public class EnemyHunterCanon : Weapon
{
    public EnemyHunterCanon(GameObject owner) : base(owner)
    {
        Stats = new WeaponStats
        {
            Damage = 10f,
            RateOfFire = 0.3f,
            Lifetime = 2f,
            Speed = 2f,
            PierceRate = 0f,
            Projectiles = 1,
        };
        this.Projectile = GameManager.Instance.EnemyStandardBullet;
    }
}
