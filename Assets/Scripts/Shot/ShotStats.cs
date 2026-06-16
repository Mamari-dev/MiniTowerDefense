using UnityEngine;


public abstract class ShotStats : ScriptableObject
{
    [SerializeField] private ProjectileTypes projectileType;
    private TowerDamageType damageType;
    private float damage;

    public ProjectileTypes ProjectileType { get => projectileType; set => projectileType = value; }
    public TowerDamageType DamageType { get => damageType; set => damageType = value; }
    public float Damage { get => damage; set => damage = value; }
}
