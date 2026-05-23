using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private TowerAttackTypes enemyTargetType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int damage;
    [SerializeField] private CurrencyTypes currencyType;
    [SerializeField] private int currencyDropAmount;

    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
    public TowerAttackTypes EnemyTargetType { get => enemyTargetType; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float BaseMoveSpeed { get => baseMoveSpeed; set => baseMoveSpeed = value; }
    public float CurrentMoveSpeed { get => currentMoveSpeed; set => currentMoveSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public int Damage { get => damage; set => damage = value; }
    public CurrencyTypes CurrencyType { get => currencyType; set => currencyType = value; }
    public int CurrencyDropAmount { get => currencyDropAmount; set => currencyDropAmount = value; }
}
