using UnityEngine;

public abstract class TowerCombatStats : ScriptableObject
{
    [Header("Tower Upgrade Values")]
    public int currentLevel = 1;
    public int maxLevel;
    public CurrencyTypes upgradeCurrencyType;
    public Color currencyColor;
    public int upgradeCost;

    public int upgradeCostScaling;
}
