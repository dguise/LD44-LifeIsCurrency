using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class UpgradeGuiHandler : MonoBehaviour
{
    Upgrade upgrade { get; set; }
    Player player { get; set; }
    UpgradeManager um { get; set; }

    TextMeshProUGUI[] texts;
    Button btn;
    private const int MAX_LEVEL = 10;

    public void Initialize(Upgrade upg, Player player, UpgradeManager upgradeManager)
    {
        upgrade = upg;
        this.player = player;
        um = upgradeManager;

        texts = GetComponentsInChildren<TextMeshProUGUI>();
        btn = GetComponentInChildren<Button>();

        SetTexts();
    }

    void SetTexts()
    {
        texts[1].text = upgrade.Title;
        texts[2].text = upgrade.Description;
        texts[3].text = upgrade.Cost + " HP";
        RefreshAvailability();

        btn.onClick.AddListener(() => {
            Upgrade(player, upgrade, texts[0]);
        });
    }

    private void Upgrade(Player player, Upgrade upgrade, TextMeshProUGUI currentLevelText)
    {
        upgrade.Purchase(player);
        upgrade.CurrentLevel++;
        currentLevelText.text = upgrade.CurrentLevel == MAX_LEVEL ? "MAX" : upgrade.CurrentLevel.ToString();

        player.Health -= upgrade.Cost;
        um.RefreshAvailability();
    }

    public void RefreshAvailability()
    {
        btn.interactable = player.Health > upgrade.Cost && upgrade.CurrentLevel < MAX_LEVEL;
    }
}
