using UnityEngine;
using System.Collections;

public class HunterEnemy : Enemy
{

    void Start()
    {
        weapon = new EnemyHunterCanon(gameObject);
    }
}
