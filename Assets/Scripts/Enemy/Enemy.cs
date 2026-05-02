using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStats stats;
    private EnemyStats copyStats;

    #region Editor
    public EnemyStats Stats { get => stats; set => stats = value; }
    public EnemyStats CopyStats { get => copyStats; set => copyStats = value; }
    [HideInInspector] public bool foldout;
    #endregion

    private void Start()
    {
        copyStats = Instantiate(stats);
        copyStats.currentHealth = copyStats.maxHealth;
    }

    public void Damage(float damage)
    {
        Debug.Log("get damage");
        copyStats.currentHealth -= damage;

        if (copyStats.currentHealth <= 0)
            gameObject.SetActive(false);
    }
}
