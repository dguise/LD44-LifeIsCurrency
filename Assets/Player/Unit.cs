using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : MonoBehaviour
{
    public GameObject HealthBar;
    float HealthBarStartingScale { get; set; }

    public GameObject ArmorBar;
    float ArmorBarStartingScale { get; set; } = 1;

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
            if (HealthBar != null)
            {
                var scale = HealthBar.transform.localScale;
                scale.x = HealthBarStartingScale * (Health / MaxHealth);
                HealthBar.transform.localScale = scale;
            }
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
            if (ArmorBar != null)
            {
                var scale = ArmorBar.transform.localScale;
                scale.x = ArmorBarStartingScale * (Armor / (MaxArmor + 0.0001f));
                ArmorBar.transform.localScale = scale;

            }

        }
    }
    private bool shouldRechargeArmor = true;

    private void Awake()
    {
        if (HealthBar == null)
            HealthBar = transform.GetChild(0)?.gameObject;

        if (HealthBar != null)
            HealthBarStartingScale = HealthBar?.transform.localScale.x ?? 0f;

        if (HasArmor && ArmorBar == null && transform.childCount > 1)
            ArmorBar = transform.GetChild(1)?.gameObject;

        if (ArmorBar != null)
            ArmorBarStartingScale = ArmorBar?.transform.localScale.x ?? 0f;

        Health = MaxHealth;
        Armor = MaxArmor;

        if (HasArmor)
            StartCoroutine(ArmorUpdate());
    }

    IEnumerator ArmorUpdate()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (shouldRechargeArmor && Armor != MaxArmor)
            {
                Armor += ArmorPerSecond * Time.deltaTime;
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
