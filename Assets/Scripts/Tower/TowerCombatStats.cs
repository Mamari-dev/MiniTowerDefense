using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatStats", menuName = "Scriptable Objects/TowerCombatStats")]
public class TowerCombatStats : ScriptableObject
{
    [Header("Tower Upgrade Values")]
    public int currentLevel = 1;
    public int maxLevel;
    public CurrencyTypes upgradeCurrencyType;
    public Color currencyColor;
    public int upgradeCost;

    public int upgradeCostScaling;


    [Header("Tower Combat Values")]
    public int targetAmount;
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;

    public float levelUpAttackScaling;
    public float levelUpSpeedScaling;

    public TowerAttackPattern attackPattern;
    public TowerAttackTypes attackTargetTypes;

    [Header("Projectile Values")]
    public ProjectileTypes projectileType;
    public float projecttileSpeed;

}