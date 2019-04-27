using UnityEngine;
using System.Collections;

public class PlayerCanon : Weapon {

    public PlayerCanon(GameObject owner): base(owner)
    {
        this.Damage = 10;
        this.RateOfFire = 1;
        this.Projectile = GameManager.instance.StandardBullet;
    }

}
