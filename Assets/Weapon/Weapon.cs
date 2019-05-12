using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Stats
    public WeaponStats Stats = new WeaponStats();

    // Utility
    public GameObject Owner { get; set; }
    public bool IsSpawner { get; set; } = false;
    protected GameObject Projectile { get; set; }

    // Constants
    private const float PROJECTILE_SPREAD = 15;

    // Logic stuff
    private int projectileLayer = 0;

    public Weapon(GameObject owner)
    {
        Owner = owner;

        if (Owner.layer == LayerConstants.GetLayer("Player"))
        {
            projectileLayer = LayerConstants.GetLayer(LayerConstants.PlayerProjectiles);
        }
        else
        {
            projectileLayer = LayerConstants.GetLayer(LayerConstants.EnemyProjectiles);
        }

    }

    float nextFire = 0;
    public void Shoot(Vector2 dir)
    {
        if (Time.time < nextFire) return;
        nextFire = Time.time + (1/(Stats.RateOfFire + 0.1f));
        var attackDir = dir;
        if (Stats.Projectiles > 1)
            attackDir = attackDir.MaakepRotate(-(PROJECTILE_SPREAD * Stats.Projectiles / 2));

        for (int i = 0; i < Stats.Projectiles; i++)
        {
            var obj = Projectile.Spawn(Owner.transform.position);
            if (IsSpawner)
                obj.transform.parent = LevelManager.Instance.transform;
            obj.layer = projectileLayer;
            var rb = obj.GetComponent<Rigidbody2D>();
            var proj = obj.GetComponent<Projectile>();
            if (proj != null)
            {
                proj.Initialize(this);
                rb.velocity = attackDir.normalized * Stats.Speed;

                attackDir = attackDir.MaakepRotate(PROJECTILE_SPREAD);
            }
        }
    }
}
