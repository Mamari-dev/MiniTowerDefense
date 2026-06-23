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

    protected string maxLevelString = "Max";

    protected TowerCombatStats towerCombatStats;
    private CircleCollider2D circleCollider;
    private AttackRangeVisual attackRangeVisual;

    protected override void Start()
    {
        base.Start();

        circleCollider = GetComponentInParent<CircleCollider2D>();
        attackRangeVisual = transform.parent.GetComponentInChildren<AttackRangeVisual>();

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
        CurrencyManager.Instance.BuyTower(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost);

        towerCombatStats.currentLevel++;
        towerCombatStats.upgradeCost += towerCombatStats.upgradeCostScaling;

        towerCurrentLevel.text = towerCombatStats.currentLevel.ToString();

        if (towerCombatStats.currentLevel == towerCombatStats.maxLevel)
            upgradeButton.interactable = false;

        circleCollider.radius = towerCombatStats.attackRange;

        attackRangeVisual.DrawAttackRange(towerCombatStats.attackRange);
    }

    protected bool Upgradeable()
    {
        if (CurrencyManager.Instance == null || !CurrencyManager.Instance.CheckCurrencyAmount(towerCombatStats.upgradeCurrencyType, towerCombatStats.upgradeCost))
            return false;
        return true;
    }

    private void LockButton()
    {
        upgradeButton.interactable = !upgradeButton.interactable;
    }
}
