﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Stats
    public WeaponStats Stats = new WeaponStats();

    // Utility
    public GameObject Owner { get; set; }
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

    public void Shoot(Vector2 dir)
    {
        var attackDir = dir;
        if (Stats.Projectiles > 1)
            attackDir = attackDir.MaakepRotate(-(PROJECTILE_SPREAD * Stats.Projectiles / 2));

        for (int i = 0; i < Stats.Projectiles; i++)
        {
            var obj = Projectile.Spawn(Owner.transform.position);
            obj.layer = projectileLayer;
            var rb = obj.GetComponent<Rigidbody2D>();
            var proj = obj.GetComponent<Projectile>();
            proj.Initialize(this);
            rb.velocity = attackDir.normalized * Stats.Speed;

            attackDir = attackDir.MaakepRotate(PROJECTILE_SPREAD);
        }
    }
}
