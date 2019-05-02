using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleUiStats : MonoBehaviour
{
    public static HandleUiStats Instance = null;

    public GameObject Skillbar;
    public GameObject Healthbar;
    public TextMeshProUGUI HealthText;
    public GameObject Armorbar;
    public TextMeshProUGUI ArmorText;

    Player player;

    Vector2 healthbarScale;
    Vector2 armorbarScale;

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

    }
}
