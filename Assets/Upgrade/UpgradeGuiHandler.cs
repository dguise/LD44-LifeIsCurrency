using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeGuiHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Upgrade upgrade { get; set; }
    Player player { get; set; }
    UpgradeManager um { get; set; }

    TextMeshProUGUI[] texts;
    GameObject description;
    Button btn;
    private const int MAX_LEVEL = 10;

    public void Initialize(Upgrade upg, Player player, UpgradeManager upgradeManager)
    {
        upgrade = upg;
        this.player = player;
        um = upgradeManager;

        texts = GetComponentsInChildren<TextMeshProUGUI>();
        description = transform.Find("DescriptionBackground").gameObject;
        btn = GetComponentInChildren<Button>();
        var icon = transform.Find("Icon").GetComponent<Image>();
        icon.color = TypeToColor(upg.Type);

        SetTexts();
    }

    void SetTexts()
    {
        texts[0].text = upgrade.Title;
        texts[1].text = upgrade.Description;
        texts[2].text = upgrade.Cost + " HP";

        description.SetActive(false);
        RefreshAvailability();

        btn.onClick.AddListener(() => {
            Upgrade(player, upgrade, texts[3]);
        });
    }

    private void Upgrade(Player player, Upgrade upgrade, TextMeshProUGUI currentLevelText)
    {
        upgrade.Purchase(player);
        upgrade.CurrentLevel++;
        currentLevelText.text = upgrade.CurrentLevel == MAX_LEVEL ? "MAX" : upgrade.CurrentLevel.ToString();

        player.ReduceHp(upgrade.Cost);

        // UI
        um.RefreshAvailability();
        HandleUiStats.Instance.handleStatChange();
        HandleUiStats.Instance.handleHpChange();
    }

    public void RefreshAvailability()
    {
        btn.interactable = player.Health > upgrade.Cost && upgrade.CurrentLevel < MAX_LEVEL;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        description.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        description.SetActive(false);
    }

    Color c_Offensive = new Color(255, 0, 0);
    Color c_Defensive = new Color(0, 0, 255);
    Color c_Misc = new Color(100, 100, 50);
    private Color TypeToColor(UpgradeType upg)
    {
        switch (upg)
        {
            case UpgradeType.Offensive:
                return c_Offensive;
            case UpgradeType.Defensive:
                return c_Defensive;
            case UpgradeType.Misc:
                return c_Misc;
            default:
                return Color.black;
        }
    }
}
