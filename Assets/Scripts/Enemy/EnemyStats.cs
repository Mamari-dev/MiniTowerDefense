using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;

    public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public float Damage { get => damage; set => damage = value; }
}
