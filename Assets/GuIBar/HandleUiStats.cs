using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HandleUiStats : MonoBehaviour
{
    public static HandleUiStats Instance = null;

    public GameObject Skillbar;
    public GameObject Healthbar;
    public TextMeshProUGUI HealthText;
    public GameObject Armorbar;
    public TextMeshProUGUI ArmorText;
    public GameObject StatText;

    Player player;

    Vector2 healthbarScale;
    Vector2 armorbarScale;

    GuiStat[] guiStats;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        player = GameManager.Player;
        healthbarScale = Healthbar.transform.localScale;
        armorbarScale = Armorbar.transform.localScale;

        guiStats = new GuiStat[] {
            new GuiStat("Damage", () => player.Weapon.Stats.Damage.ToString("N1")),
            new GuiStat("Rate of fire", () => player.Weapon.Stats.RateOfFire.ToString("N1") + "/s"),
            new GuiStat("Bullet speed", () => player.Weapon.Stats.Speed.ToString("N1")),
            new GuiStat("Bullet lifetime", () => player.Weapon.Stats.Lifetime.ToString("N1") + "s"),
            new GuiStat("Pierce rate", () => player.Weapon.Stats.PierceRate.ToString("P")),
            new GuiStat("Projectiles", () => player.Weapon.Stats.Projectiles.ToString("N0")),
            new GuiStat("Armor cooldown", () => player.ArmorRechargeCooldown.ToString("N1")),
            new GuiStat("Armor refill", () => player.ArmorPerSecond.ToString("N1") + "/s"),
            new GuiStat("Armor resistance", () => player.ArmorDamageReduction.ToString("P")),
            new GuiStat("Speed", () => player.Movementspeed.ToString("N1")),
            new GuiStat("HP / round", () => player.RepairPerRound.ToString("N1")),
        };

        foreach (var stat in guiStats)
        {
            var obj = Instantiate<GameObject>(StatText, Skillbar.transform);
            stat.TextObject = obj.GetComponent<Text>();
        }

        handleHpChange();
        handleStatChange();
    }

    public void handleHpChange()
    {
        handleArmorChange();

        HealthText.text = $"{Mathf.RoundToInt(player.Health)} / {player.MaxHealth}";
        healthbarScale.y = Mathf.Clamp(player.Health / player.MaxHealth, 0, 1);
        Healthbar.transform.localScale = healthbarScale;
    }

    public void handleArmorChange()
    {
        ArmorText.text = $"{Mathf.RoundToInt(player.Armor)} / {player.MaxArmor}";
        armorbarScale.y = Mathf.Clamp(player.Armor / (player.MaxArmor+0.00001f), 0, 1);
        Armorbar.transform.localScale = armorbarScale;
    }

    public void handleStatChange()
    {
        foreach (var stat in guiStats)
        {
            stat.RefreshText();
        }
    }
}

class GuiStat
{
    public string Title { get; set; }
    public System.Func<string> Value { get; set; }
    public Text TextObject { get; set; } // Is set when generating all objects

    public GuiStat(string title, System.Func<string> callback)
    {
        this.Title = title;
        this.Value = callback;
    }

    public string GetPresentationString()
    {
        return $"{Title}: {Value()}";
    }
    
    public void RefreshText()
    {
        TextObject.text = GetPresentationString();
    }
}