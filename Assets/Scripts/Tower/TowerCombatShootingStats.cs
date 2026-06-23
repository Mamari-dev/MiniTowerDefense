using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatShootingStats", menuName = "Scriptable Objects/Tower/TowerCombatShootingStats")]
public class TowerCombatShootingStats : TowerCombatStats
{
    public int targetAmount;

    public TowerAttackPattern attackPattern;
    public TowerAttackTypes attackTargetTypes;

    [Header("Projectile Values")]
    public ShotTypes shotType;
    public float shotSpeed;
}