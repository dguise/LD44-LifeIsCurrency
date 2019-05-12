using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Weapon weapon;

    override protected void Die()
    {
        LevelManager.Instance.OnEnemyDied();
        Destroy(gameObject);
    }
}
