using UnityEngine;

public abstract class TowerCombatPulsing : TowerCombat
{
    [SerializeField] private TowerCombatPulsingStats towerCombatStats;
    protected TowerCombatPulsingStats towerRunTimeCombatStats;

    #region Editor
    public TowerCombatPulsingStats TowerCombatStats { get => towerCombatStats; }
    public TowerCombatPulsingStats TowerRunTimeCombatStats { get => towerRunTimeCombatStats; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        towerRunTimeCombatStats = Instantiate(towerCombatStats);
        towerCollider.radius = towerRunTimeCombatStats.attackRange;
        attackRangeVisual.DrawAttackRange(towerRunTimeCombatStats.attackRange);
    }

    public override float GetAttackRange()
    {
        return towerCombatStats.attackRange;
    }
}
