using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatShootingStats", menuName = "Scriptable Objects/TowerCombatShootingStats")]
public class TowerCombatShootingStats : TowerCombatStats
{
    public int targetAmount;

    public TowerAttackPattern attackPattern;
    public TowerAttackTypes attackTargetTypes;

    [Header("Projectile Values")]
    public ProjectileTypes projectileType;
    public float projecttileSpeed;
}