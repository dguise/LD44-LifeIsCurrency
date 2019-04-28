using UnityEngine;
using System.Collections;

public class CustomEnemyCanon : Weapon
{
    public CustomEnemyCanon(GameObject owner, 
        float d, 
        float rate, 
        float lif, 
        float spd, 
        float piers, 
        int projs, 
        GameObject projectile) : base(owner)
    {
        Stats = new WeaponStats
        {
            Damage = d,
            RateOfFire = rate,
            Lifetime = lif,
            Speed = spd,
            PierceRate = piers,
            Projectiles = projs,
        };
        this.Projectile = projectile;
    }
}
