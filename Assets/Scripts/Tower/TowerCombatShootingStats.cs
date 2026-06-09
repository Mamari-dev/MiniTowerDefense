using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatShootingStats", menuName = "Scriptable Objects/TowerCombatShootingStats")]
public class TowerCombatShootingStats : TowerCombatStats
{
    [Header("Tower Combat Values")]
    public int targetAmount;
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;

    public float levelUpRangeScaling;
    public float levelUpAttackScaling;
    public float levelUpSpeedScaling;

    public TowerAttackPattern attackPattern;
    public TowerAttackTypes attackTargetTypes;

    [Header("Projectile Values")]
    public ProjectileTypes projectileType;
    public float projecttileSpeed;
}