using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEnemy : Enemy
{
    void Start()
    {
        weapon = new EnemySiegeCanon(gameObject.GetComponentInChildren<ParticleSystem>().gameObject);
    }
}
