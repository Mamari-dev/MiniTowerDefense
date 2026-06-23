using UnityEngine;


public abstract class ShotStats : ScriptableObject
{
    [SerializeField] private ShotTypes projectileType;
    private TowerDamageType damageType;
    private float damage;

    public ShotTypes ProjectileType { get => projectileType; set => projectileType = value; }
    public TowerDamageType DamageType { get => damageType; set => damageType = value; }
    public float Damage { get => damage; set => damage = value; }
}
