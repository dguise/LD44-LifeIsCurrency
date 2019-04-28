using UnityEngine;
using System.Collections;

public class PlayerCanon : Weapon {

    public PlayerCanon(GameObject owner): base(owner)
    {
        Stats = new WeaponStats
        {
            Projectiles = 1,
            Speed = 3,
            Lifetime = 1f,
            RateOfFire = 1f,
            Damage = 10f,
            PierceRate = 0f,
        };
        this.Projectile = GameManager.Instance.StandardBullet;
    }

}
