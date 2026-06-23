using TMPro;
using UnityEngine;

public class TowerCombatPulsingCanvas : TowerCombatCanvas
{
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] private TextMeshProUGUI nextAttackValue;
    [SerializeField] private TextMeshProUGUI attackSpeedValue;
    [SerializeField] private TextMeshProUGUI nextAttackSpeedValue;
    [SerializeField] private TextMeshProUGUI attackRangeValue;
    [SerializeField] private TextMeshProUGUI nextAttackRangeValue;

    private TowerCombatPulsingStats towerCombatPulsingStats;

    protected override void Start()
    {
        TowerCombatPulsing towerCombatPulsing = GetComponentInParent<TowerCombatPulsing>();
        if (towerCombatPulsing == null) return;

        towerCombatPulsingStats = towerCombatPulsing.TowerRunTimeCombatStats;
        towerCombatStats = towerCombatPulsingStats;

        base.Start();
        UpdateNextLevelStats();
    }

    protected override void UpdateCombatStats()
    {
        base.UpdateCombatStats();
        attackValue.text = towerCombatPulsingStats.attackDamage.ToString("F2");
        attackSpeedValue.text = towerCombatPulsingStats.attackSpeed.ToString("F2");
        attackRangeValue.text = towerCombatPulsingStats.attackRange.ToString("F2");
    }

    private void UpdateNextLevelStats()
    {
        if (towerCombatStats.currentLevel != towerCombatStats.maxLevel)
        {
            nextAttackValue.text = (towerCombatPulsingStats.attackDamage * towerCombatPulsingStats.levelUpAttackScaling).ToString("F2");
            nextAttackSpeedValue.text = (towerCombatPulsingStats.attackSpeed * towerCombatPulsingStats.levelUpSpeedScaling).ToString("F2");
            nextAttackRangeValue.text = (towerCombatPulsingStats.attackRange * towerCombatPulsingStats.levelUpRangeScaling).ToString("F2");
        }
        else
        {
            nextAttackValue.text = maxLevelString;
            nextAttackSpeedValue.text = maxLevelString;
            nextAttackRangeValue.text = maxLevelString;
        }
    }

    public override void OnUpgrade()
    {
        if (!Upgradeable()) return;

        towerCombatPulsingStats.attackDamage *= towerCombatPulsingStats.levelUpAttackScaling;
        towerCombatPulsingStats.attackSpeed *= towerCombatPulsingStats.levelUpSpeedScaling;
        towerCombatPulsingStats.attackRange *= towerCombatPulsingStats.levelUpRangeScaling;

        base.OnUpgrade();

        UpdateCombatStats();
        UpdateNextLevelStats();
    }
}
