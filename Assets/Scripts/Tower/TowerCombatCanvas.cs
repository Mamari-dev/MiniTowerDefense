using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCombatCanvas : TowerNonCombatCanvas
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI towerCurrentLevel;
    [SerializeField] private TextMeshProUGUI towerMaxLevel;
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] private TextMeshProUGUI attackSpeedValue;
    [SerializeField] private TextMeshProUGUI upgradeCostValue;

    //private TowerBaseStats towerBaseStats;
    private TowerCombatStats towerCombatStats;

    protected override void Start()
    {
        base.Start();
        towerCombatStats = GetComponentInParent<TowerCombat>().TowerRunTimeCombatStats;

        UpdateCombatStats();
    }

    private void UpdateCombatStats()
    {
        towerCurrentLevel.text = towerCombatStats.currentLevel.ToString();
        towerMaxLevel.text = towerCombatStats.maxLevel.ToString();
        upgradeCostValue.text = towerCombatStats.upgradeCost.ToString();

        attackValue.text = towerCombatStats.attackDamage.ToString();
        attackSpeedValue.text = towerCombatStats.attackSpeed.ToString();
    }

    public void OnUpgrade()
    {
        if (!CurrencyManager.Instance.CheckCurrencyAmount(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost)) return;

        CurrencyManager.Instance.BuyTower(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost);

        towerCombatStats.currentLevel++;
        towerCombatStats.upgradeCost += towerCombatStats.upgradeCostScaling;
        towerCombatStats.attackDamage = Mathf.Round(towerCombatStats.attackDamage * towerCombatStats.levelUpAttackScaling * 10) * 0.1f;
        towerCombatStats.attackSpeed = Mathf.Round((towerCombatStats.attackSpeed *= towerCombatStats.levelUpSpeedScaling) * 10) * 0.1f;

        towerCurrentLevel.text = towerCombatStats.currentLevel.ToString();
        UpdateCombatStats();

        if (towerCombatStats.currentLevel == towerCombatStats.maxLevel)
            button.interactable = false;
    }
}
