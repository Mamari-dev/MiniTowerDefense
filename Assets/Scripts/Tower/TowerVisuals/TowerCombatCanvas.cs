using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class TowerCombatCanvas : TowerNonCombatCanvas
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI towerCurrentLevel;
    [SerializeField] private TextMeshProUGUI towerMaxLevel;
    
    [SerializeField] private TextMeshProUGUI upgradeCostValue;
    [SerializeField] private Image[] upgradeImages;

    protected TowerCombatStats towerCombatStats;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < upgradeImages.Length; i++)
        {
            upgradeImages[i].color = towerCombatStats.currencyColor;
        }

        UpdateCombatStats();

        WaveManager.StartWave += LockButton;
        WaveManager.EndWave += LockButton;
    }

    protected virtual void UpdateCombatStats()
    {
        towerCurrentLevel.text = towerCombatStats.currentLevel.ToString();
        towerMaxLevel.text = towerCombatStats.maxLevel.ToString();
        upgradeCostValue.text = towerCombatStats.upgradeCost.ToString();
    }

    public virtual void OnUpgrade()
    {
        if (CurrencyManager.Instance == null || !CurrencyManager.Instance.CheckCurrencyAmount(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost)) return;

        CurrencyManager.Instance.BuyTower(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost);

        towerCombatStats.currentLevel++;
        towerCombatStats.upgradeCost += towerCombatStats.upgradeCostScaling;

        towerCurrentLevel.text = towerCombatStats.currentLevel.ToString();
        UpdateCombatStats();

        if (towerCombatStats.currentLevel == towerCombatStats.maxLevel)
            upgradeButton.interactable = false;
    }

    private void LockButton()
    {
        upgradeButton.interactable = !upgradeButton.interactable;
    }
}
