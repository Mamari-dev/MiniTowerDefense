using UnityEngine;

[CreateAssetMenu(fileName = "Tower_SingleAttack_SO", menuName = "Scriptable Objects/Tower_SingleAttack_SO")]
public class Tower_SingleAttackStats : ScriptableObject
{
    [Header("Tower Values")]
    public float attackRange;
    public float attackSpeed;

    [Header("Projectile Values")]
    public float projectileDamage;
    public float projectileSpeed;
}
