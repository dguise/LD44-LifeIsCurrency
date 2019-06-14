using UnityEngine;
using System.Collections;

public class PlayerCanon : Weapon {

    public PlayerCanon(GameObject owner): base(owner)
    {
        Stats = new WeaponStats
        {
            Damage = GameData.PlayerWeaponStartingStats.Damage,
            Lifetime = GameData.PlayerWeaponStartingStats.Lifetime,
            PierceRate = GameData.PlayerWeaponStartingStats.PierceRate,
            Projectiles = GameData.PlayerWeaponStartingStats.Projectiles,
            RateOfFire = GameData.PlayerWeaponStartingStats.RateOfFire,
            Speed = GameData.PlayerWeaponStartingStats.Speed,
        };
        this.Projectile = GameManager.Instance.StandardBullet;
    }

}
