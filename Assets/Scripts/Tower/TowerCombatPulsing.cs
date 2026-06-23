using UnityEngine;

public abstract class TowerCombatPulsing : TowerCombat
{
    protected TowerCombatPulsingStats towerRunTimeCombatStats;
    public TowerCombatPulsingStats TowerRunTimeCombatStats { get => towerRunTimeCombatStats; }


    protected override void Awake()
    {
        base.Awake();
        towerRunTimeCombatStats = (TowerCombatPulsingStats)towerRunTimeBaseStats;
        towerCollider.radius = towerRunTimeCombatStats.attackRange;
        attackRangeVisual.DrawAttackRange(towerRunTimeCombatStats.attackRange);
    }

    public override float GetAttackRange()
    {
        if (towerRunTimeCombatStats == null)
        {
            TowerCombatStats combatStat = (TowerCombatStats)TowerBaseStats;
            return combatStat.attackRange;
        }

        return towerRunTimeCombatStats.attackRange;
    }
}
