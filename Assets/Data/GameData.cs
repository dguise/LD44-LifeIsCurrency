using UnityEngine;
using System.Collections;

public static class GameData
{
    public static WeaponStats PlayerWeaponStartingStats = new WeaponStats
    {
        Projectiles = 1,
        Speed = 3,
        Lifetime = 1f,
        RateOfFire = 1f,
        Damage = 10f,
        PierceRate = 0f,
    };
}
