using UnityEngine;
using System.Collections;
using System;

public abstract class Unit : MonoBehaviour
{
    public GameObject HealthBar;
    float HealthBarStartingScale { get; set; }

    public float Movementspeed = 2;
    public float MaxHealth = 100;
    private float _Health;
    public float Health
    {
        get
        {
            return _Health;
        }
        set
        {
            _Health = Mathf.Clamp(value, 0, MaxHealth);
            HealthBar.transform.localScale = new Vector2(HealthBarStartingScale * (Health / MaxHealth), HealthBar.transform.localScale.y);
        }
    }

    private void Awake()
    {
        if (HealthBar == null)
            HealthBar = transform.GetChild(0).gameObject;

        HealthBarStartingScale = HealthBar.transform.localScale.x;

        _Health = MaxHealth;
    }

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
            Die();
    }

    abstract protected void Die();
}
