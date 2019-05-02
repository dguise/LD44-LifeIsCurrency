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
        private set
        {
            _Health = Mathf.Clamp(value, 0, MaxHealth);
            HealthBar.transform.localScale = new Vector2(HealthBarStartingScale * (Health / MaxHealth), HealthBar.transform.localScale.y);
        }
    }


    [Header("Armor")]
    public bool HasArmor = false;
    public float ArmorDamageReduction { get; set; } = 0;
    public float ArmorRechargeCooldown  = 10;
    public float MaxArmor = 0;
    public float ArmorPerSecond = 1;
    private float _Armor = 0;
    public float Armor
    {
        get
        {
            return _Armor;
        }
        set
        {
            _Armor = Mathf.Clamp(value, 0, MaxArmor);
        }
    }
    private bool shouldRechargeArmor = true;

    private void Awake()
    {
        if (HealthBar == null)
            HealthBar = transform.GetChild(0).gameObject;

        HealthBarStartingScale = HealthBar.transform.localScale.x;

        _Health = MaxHealth;

        if (HasArmor)
            StartCoroutine(ArmorUpdate());
    }

    IEnumerator ArmorUpdate()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (shouldRechargeArmor && Armor != MaxArmor)
            {
                Armor += ArmorPerSecond * Time.fixedDeltaTime;
            }
        }
    }



    public bool IsDead
    {
        get
        {
            return Health <= 0;
        }
    }

    IEnumerator DisableArmorRecharing()
    {
        shouldRechargeArmor = false;
        yield return new WaitForSeconds(ArmorRechargeCooldown);
        shouldRechargeArmor = true;
    }

    Coroutine disableArmorRecharing = null;
    public virtual void TakeDamage(float damage)
    {
        if (HasArmor)
            damage = DamageArmorAndGetLeftoverDamage(damage);

        if (damage > 0)
            ReduceHp(damage);

        if (IsDead)
            Die();


    }

    private float DamageArmorAndGetLeftoverDamage(float damage)
    {
        var damageLeftAfterArmor = damage;
        if (Armor > 0)
        {
            var armorDmg = damage * (1 - ArmorDamageReduction);
            damageLeftAfterArmor = armorDmg - Armor;
            Armor -= armorDmg;
        }
        if (disableArmorRecharing != null)
            StopCoroutine(disableArmorRecharing);
        disableArmorRecharing = StartCoroutine(DisableArmorRecharing());
        return damageLeftAfterArmor;
    }

    public virtual void ReduceHp(float hp)
    {
        Health -= hp;
    }

    abstract protected void Die();
}
