using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public GameObject screen;
    public GameObject upgradeGui;
    public Button toggleUpgradesButton;

    public static List<Upgrade> Upgrades = new List<Upgrade>()
    {
        new Upgrade()
        {
            Title = "Damage",
            Description = "Permanently upgrades your damage by 10",
            Cost = 20f,
            Purchase = (p) =>
            {
                p.Weapon.Damage += 10;
            }
        },
        new Upgrade()
        {
            Title = "Pierce",
            Description = "Upgrades your pierce chance by 10%",
            Cost = 20f,
            Purchase = (p) =>
            {
                p.Weapon.PierceRate += 0.1f;
            }
        },
        new Upgrade()
        {
            Title = "Projectiles",
            Description = "Adds a projectile to your standard attack",
            Cost = 30f,
            Purchase = (p) =>
            {
                p.Weapon.Projectiles += 1;
            }
        },
        new Upgrade()
        {
            Title = "Projectile speed",
            Description = "Increases projectile speed by 30",
            Cost = 10f,
            Purchase = (p) =>
            {
                p.Weapon.Speed += 30;
            }
        },
    };

    void Start()
    {
        toggleUpgradesButton.onClick.AddListener(ToggleActive);

        var player = GameObject.FindObjectOfType<Player>();
        foreach (var upgrade in Upgrades)
        {
            var obj = Instantiate<GameObject>(upgradeGui, screen.transform);
            var texts = obj.GetComponentsInChildren<TextMeshProUGUI>();
            texts[1].text = upgrade.Title;
            texts[2].text = upgrade.Description;
            texts[3].text = upgrade.Cost + " HP";
            var btn = obj.GetComponentInChildren<Button>();
            btn.interactable = player.Health > upgrade.Cost;

            btn.onClick.AddListener(() => {
                Upgrade(player, upgrade, texts[0]);
            });
        }
    }

    private void ToggleActive()
    {
        screen.SetActive(!screen.activeInHierarchy);
    }

    private void Upgrade(Player player, Upgrade upgrade, TextMeshProUGUI currentLevelText)
    {
        if (player.Health > upgrade.Cost)
        {
            Debug.Log("Upgraded " + upgrade.Title);
            upgrade.Purchase(player);
            player.Health -= upgrade.Cost;
            currentLevelText.text = (int.Parse(currentLevelText.text) + 1).ToString();
        }
    }

    public void Show()
    {
        screen.SetActive(true);
    }

    public void Hide()
    {
        screen.SetActive(false);
    }
}
