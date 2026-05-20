using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatStats", menuName = "Scriptable Objects/TowerCombatStats")]
public class TowerCombatStats : ScriptableObject
{
    [Header("Tower Upgrade Values")]
    public int currentLevel = 1;
    public int maxLevel;
    public CurrencyTypes upgradeCurrencyType;
    public int upgradeCost;

    public int upgradeCostScaling;


    [Header("Tower Combat Values")]
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;
    public float projecttileSpeed;

    public float levelUpAttackScaling;
    public float levelUpSpeedScaling;

}