using UnityEngine;
using System.Collections;

public class PlayerCanon : Weapon {

    public PlayerCanon(GameObject owner): base(owner)
    {
        Stats = new WeaponStats
        {
            Projectiles = 1,
            Speed = 1,
            Lifetime = 1f,
        };
        this.Projectile = GameManager.Instance.StandardBullet;
    }

}
