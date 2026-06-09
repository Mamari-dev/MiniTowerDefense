using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatPulsingStats", menuName = "Scriptable Objects/TowerCombatPulsingStats")]
public class TowerCombatPulsingStats : TowerCombatStats
{
    [Header("Tower Combat Values")]
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;

    public float levelUpRangeScaling;
    public float levelUpAttackScaling;
    public float levelUpSpeedScaling;
}
