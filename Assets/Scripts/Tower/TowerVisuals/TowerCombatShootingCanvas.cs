using TMPro;
using UnityEngine;

public class TowerCombatShootingCanvas : TowerCombatCanvas
{
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] private TextMeshProUGUI nextAttackValue;
    [SerializeField] private TextMeshProUGUI attackSpeedValue;
    [SerializeField] private TextMeshProUGUI nextAttackSpeedValue;
    [SerializeField] private TextMeshProUGUI attackRangeValue;
    [SerializeField] private TextMeshProUGUI nextAttackRangeValue;

    private TowerCombatShootingStats towerCombatShootingStats;

    protected override void Start()
    {
        TowerCombatShooting towerCombatShooting = GetComponentInParent<TowerCombatShooting>();
        if (towerCombatShooting == null) return;

        towerCombatShootingStats = towerCombatShooting.TowerRunTimeCombatStats;
        towerCombatStats = towerCombatShootingStats;

        base.Start();
        UpdateNextLevelStats();
    }

    protected override void UpdateCombatStats()
    {
        base.UpdateCombatStats();
        attackValue.text = towerCombatShootingStats.attackDamage.ToString("F2");
        attackSpeedValue.text = towerCombatShootingStats.attackSpeed.ToString("F2");
        attackRangeValue.text = towerCombatShootingStats.attackRange.ToString("F2");
    }

    private void UpdateNextLevelStats()
    {
        nextAttackValue.text = (towerCombatShootingStats.attackDamage * towerCombatShootingStats.levelUpAttackScaling).ToString("F2");
        nextAttackSpeedValue.text = (towerCombatShootingStats.attackSpeed * towerCombatShootingStats.levelUpSpeedScaling).ToString("F2");
        nextAttackRangeValue.text = (towerCombatShootingStats.attackRange * towerCombatShootingStats.levelUpRangeScaling).ToString("F2");
    }

    public override void OnUpgrade()
    {
        if (!Upgradeable()) return;

        base.OnUpgrade();

        towerCombatShootingStats.attackDamage *= towerCombatShootingStats.levelUpAttackScaling;
        towerCombatShootingStats.attackSpeed *= towerCombatShootingStats.levelUpSpeedScaling;
        towerCombatShootingStats.attackRange *= towerCombatShootingStats.levelUpRangeScaling;

        UpdateCombatStats();
        UpdateNextLevelStats();
    }
}
