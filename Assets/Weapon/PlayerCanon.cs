using UnityEngine;
using System.Collections;

public class PlayerCanon : Weapon {

    public PlayerCanon(GameObject owner): base(owner)
    {
        Stats = GameData.PlayerWeaponStartingStats;
        this.Projectile = GameManager.Instance.StandardBullet;
    }

}
