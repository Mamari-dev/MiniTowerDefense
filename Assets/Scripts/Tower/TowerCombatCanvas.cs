using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCombatCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerCurrentLevel;
    [SerializeField] private TextMeshProUGUI towerMaxLevel;
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] private TextMeshProUGUI attackSpeedValue;
    [SerializeField] private TextMeshProUGUI upgradeCostValue;

    private TowerBaseStats towerBaseStats;
    private TowerCombatStats towerCombatStats;

    private void Start()
    {
        towerBaseStats = GetComponentInParent<TowerCombat>().TowerRunTimeBaseStats;
        towerCombatStats = GetComponentInParent<TowerCombat>().TowerRunTimeCombatStats;

        UpdateBaseStats();
        UpdateCombatStats();
        EnAndDisableCanvas();
    }

    private void UpdateBaseStats()
    {
        towerName.text = towerBaseStats.towerName;
        towerCurrentLevel.text = towerBaseStats.currentLevel.ToString();
        towerMaxLevel.text = towerBaseStats.maxLevel.ToString();
        upgradeCostValue.text = towerBaseStats.upgradeCost.ToString();
    }

    private void UpdateCombatStats()
    {
        attackValue.text = towerCombatStats.attackDamage.ToString();
        attackSpeedValue.text = towerCombatStats.attackSpeed.ToString();
    }

    public void OnUpgrade()
    {
        if (!CurrencyManager.Instance.CheckCurrencyAmount(towerBaseStats.upgradeCurrencyType, towerBaseStats.upgradeCost)) return;

        CurrencyManager.Instance.BuyTower(towerBaseStats.upgradeCurrencyType, towerBaseStats.upgradeCost);

        towerBaseStats.currentLevel++;
        towerCombatStats.attackDamage = Mathf.Round(towerCombatStats.attackDamage * towerCombatStats.levelUpScaling * 10) * 0.1f;
        towerCombatStats.attackSpeed = Mathf.Round((towerCombatStats.attackSpeed *= towerCombatStats.levelUpScaling) * 10) * 0.1f;

        towerCurrentLevel.text = towerBaseStats.currentLevel.ToString();
        UpdateCombatStats();

        if (towerBaseStats.currentLevel == towerBaseStats.maxLevel)
            button.interactable = false;
    }

    public void EnAndDisableCanvas()
    {
        canvas.enabled = !canvas.enabled;
    }
}
