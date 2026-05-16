using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatStats", menuName = "Scriptable Objects/TowerCombatStats")]
public class TowerCombatStats : ScriptableObject
{
    [Header("Tower Combat Values")]
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;
    public float projecttileSpeed;
}