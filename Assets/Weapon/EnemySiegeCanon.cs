using UnityEngine;
using System.Collections;

public class EnemySiegeCanon : Weapon
{
    public EnemySiegeCanon(GameObject owner) : base(owner)
    {
        Stats = new WeaponStats
        {
            Damage = 50f,
            RateOfFire = 0.05f,
            Lifetime = 10f,
            Speed = 5f,
            PierceRate = 0f,
            Projectiles = 1,
        };
        this.Projectile = GameManager.Instance.EnemySiegeBullet;
    }
}
