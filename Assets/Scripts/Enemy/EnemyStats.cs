using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private TowerAttackTypes enemyTargetType;
    [SerializeField] private float health;
    [SerializeField] private bool armored;
    [SerializeField] private float armoredMultiplier;
    [SerializeField] private bool shielded;
    [SerializeField] private float shieldedMultiplier;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int damage;
    [SerializeField] private CurrencyTypes currencyType;
    [SerializeField] private int currencyDropAmount;
    [SerializeField] private float deathImpulseForce;
    [SerializeField] private float deathImpulseTime;

    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
    public TowerAttackTypes EnemyTargetType { get => enemyTargetType; }
    public float Health { get => health; set => health = value; }
    public bool Armored { get => armored; set => armored = value; }
    public float ArmoredMultiplier { get => armoredMultiplier; set => armoredMultiplier = value; }
    public bool Shielded { get => shielded; set => shielded = value; }
    public float ShieldedMultiplier { get => shieldedMultiplier; set => shieldedMultiplier = value; }
    public float BaseMoveSpeed { get => baseMoveSpeed; set => baseMoveSpeed = value; }
    public float CurrentMoveSpeed { get => currentMoveSpeed; set => currentMoveSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public int Damage { get => damage; set => damage = value; }
    public CurrencyTypes CurrencyType { get => currencyType; set => currencyType = value; }
    public int CurrencyDropAmount { get => currencyDropAmount; set => currencyDropAmount = value; }
    public float DeathImpulsForce { get => deathImpulseForce; set => deathImpulseForce = value; }
    public float DeathImpulseTime { get => deathImpulseTime; set => deathImpulseTime = value; }
}
