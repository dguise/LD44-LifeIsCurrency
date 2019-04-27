using UnityEngine;
using System.Collections;
using System;

public class Unit : MonoBehaviour
{
    public float Health = 100;
    public bool IsDead
    {
        get
        {
            return Health <= 0;
        }
    }

    internal void TakeDamage(float damage)
    {
        Health -= damage;
        if (IsDead)
            Destroy(gameObject);
    }
}
