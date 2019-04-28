using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{

    override protected void Die()
    {
        LevelManager.OnEnemyDied();
        Destroy(gameObject);
    }
}
