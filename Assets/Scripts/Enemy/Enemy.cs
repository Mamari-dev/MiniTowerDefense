using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyStats stats;
    private EnemyStats copyStats;

    private List<Vector3> path = new();
    private int nextPathPoint = 0;
    #region Editor
    public EnemyStats Stats { get => stats; set => stats = value; }
    public EnemyStats CopyStats { get => copyStats; set => copyStats = value; }
    [HideInInspector] public bool foldout;
    #endregion

    private void Awake()
    {
        copyStats = Instantiate(stats);
    }

    private void OnEnable()
    {
        path = MapManager.Instance.CurrentWorldPath;
        copyStats.CurrentHealth = copyStats.MaxHealth;
        nextPathPoint = 0;
    }

    private void Update()
    {
        if (nextPathPoint >= path.Count) return;

        Move();
        CheckWayPoint();
    }

    private void Move()
    {
        Vector2 dir = (path[nextPathPoint] - transform.position).normalized;
        transform.up = Vector3.Slerp(transform.up, dir, copyStats.RotationSpeed * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, path[nextPathPoint], copyStats.MoveSpeed * Time.deltaTime);
    }

    private void CheckWayPoint()
    {
        float distanceSq = (path[nextPathPoint] - transform.position).sqrMagnitude;

        if (distanceSq < 0.001f)
        {
            nextPathPoint++;

            if (nextPathPoint >= path.Count)
            {
                Debug.Log("deal player damage");
                EnemyPoolingManager.Instance.BackInPool(this.gameObject, copyStats.EnemyType);
                gameObject.SetActive(false);
            }
        }
    }

    public void Damage(float damage)
    {
        copyStats.CurrentHealth -= damage;

        if (copyStats.CurrentHealth <= 0)
        {
            EnemyPoolingManager.Instance.BackInPool(this.gameObject, copyStats.EnemyType);
            gameObject.SetActive(false);
        }
    }
}
