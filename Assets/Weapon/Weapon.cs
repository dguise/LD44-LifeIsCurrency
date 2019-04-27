using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    // Stats
    public float Damage { get; set; }
    public float RateOfFire { get; set; }
    public float Speed { get; set; }
    public int Projectiles { get; set; }
    public float PierceRate { get; set; }

    // Utility
    public GameObject Owner { get; set; }
    protected GameObject Projectile { get; set; }

    // Constants
    private const float PROJECTILE_SPREAD = 15;

    // Logic stuff
    private int projectileLayer = 0;

    public Weapon(GameObject owner)
    {
        Projectiles = 3;
        Speed = 2; 
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

    public void Shoot()
    {
        var dir = -(Vector2)(Owner.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition));
        var attackDir = dir;
        if (Projectiles > 1)
            attackDir = attackDir.MaakepRotate(-(PROJECTILE_SPREAD * Projectiles / 2));

        for (int i = 0; i < Projectiles; i++)
        {
            var obj = Projectile.Spawn(Owner.transform.position);
            obj.layer = projectileLayer;
            var rb = obj.GetComponent<Rigidbody2D>();
            var proj = obj.GetComponent<Projectile>();
            proj.Initialize(this);
            rb.velocity = attackDir.normalized * Speed;

            attackDir = attackDir.MaakepRotate(PROJECTILE_SPREAD);
        }
    }
}
