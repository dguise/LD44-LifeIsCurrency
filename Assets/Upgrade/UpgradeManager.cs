using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public GameObject screen;
    public GameObject upgradeGui;
    public Button toggleUpgradesButton;

    public static bool state_IsInUpgradeMenu = false;

    void Start()
    {
        toggleUpgradesButton.onClick.AddListener(ToggleActive);

        var player = GameObject.FindObjectOfType<Player>();
        foreach (var upgrade in Upgrades)
        {
            var obj = Instantiate<GameObject>(upgradeGui, screen.transform);
            var ugh = obj.GetComponent<UpgradeGuiHandler>();
            ugh.Initialize(upgrade, player, this);
        }
    }

    private void ToggleActive()
    {
        RefreshAvailability();
        var newActive = !screen.activeInHierarchy;
        state_IsInUpgradeMenu = newActive;
        screen.SetActive(newActive);
        Time.timeScale = newActive ? 0.2f : 1f;
    }

    public void RefreshAvailability()
    {
        foreach (Transform child in screen.transform)
        {
            var ugh = child.GetComponent<UpgradeGuiHandler>();
            ugh.RefreshAvailability();
        }
    }

    public static List<Upgrade> Upgrades = new List<Upgrade>()
    {
        new Upgrade{ Title = "Damage", Description = "Permanently upgrades your damage by 10", Cost = 20f, Purchase = (p) => {p.Weapon.Stats.Damage += 10; }},
        new Upgrade{ Title = "Rate of fire", Description = "Increases your rate of fire by 0.3", Cost = 20f, Purchase = (p) => { p.Weapon.Stats.RateOfFire += 0.3f; }},
        new Upgrade{ Title = "Pierce" , Description = "Upgrades your pierce chance by 10%", Cost = 20f, Purchase = (p) => { p.Weapon.Stats.PierceRate += 0.1f; }},
        new Upgrade{ Title = "Projectiles", Description = "Adds a projectile to your standard attack", Cost = 30f, Purchase = (p) => { p.Weapon.Stats.Projectiles += 1; }},
        new Upgrade{ Title = "Projectile speed", Description = "Increases projectile speed by 10%", Cost = 10f, Purchase = (p) => { p.Weapon.Stats.Speed *= 1.1f; }},
        new Upgrade{ Title = "Projectile duration", Description = "Increases lifetime by 0.2s", Cost = 10f, Purchase = (p) => { p.Weapon.Stats.Lifetime += 0.2f; }},
        new Upgrade{ Title = "GAMBLER DAMAGE", Description = "Increases damage by a random value between 5 & 20", Cost = 20f, Purchase = (p) => { p.Weapon.Stats.Damage += Random.Range(0f, 20f); }},
        new Upgrade{ Title = "DOUBLE DAMAGE", Description = "Doubles your damage", Cost = 70f, Purchase = (p) => { p.Weapon.Stats.Damage *= 2; }},
        new Upgrade{ Title = "Max health", Description = "Increases your max health by 10", Cost = 20f, Purchase = (p) => { p.MaxHealth += 10; }},
        new Upgrade{ Title = "Health repair", Description = "Increases your health / round repair by 10", Cost = 20f, Purchase = (p) => { p.RepairPerRound += 10; }},
        new Upgrade{ Title = "Max Armor", Description = "Increases your Armor by 10", Cost = 15f, Purchase = (p) => { p.MaxArmor += 10; }},
        new Upgrade{ Title = "Damage reduction", Description = "Reduces damage taken to armor by 8%", Cost = 15f, Purchase = (p) => { p.ArmorDamageReduction += 0.08f; }},
        new Upgrade{ Title = "Armor cooldown", Description = "Reduce Armor recharge cooldown by 0.8s", Cost = 15f, Purchase = (p) => { p.ArmorRechargeCooldown -= 0.8f; }},
        new Upgrade{ Title = "Armor recharge", Description = "Increase Armor recharge by 1 per second", Cost = 15f, Purchase = (p) => { p.ArmorPerSecond += 1f; }},
        new Upgrade{ Title = "Rocket boosters", Description = "Increases your movementspeed by 20%", Cost = 20f,Purchase = (p) => { p.Movementspeed *= 1.2f; }},
    };
}
