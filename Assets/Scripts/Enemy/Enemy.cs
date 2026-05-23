using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ISlowable
{
    [SerializeField] private EnemyStats stats;
    private EnemyStats copyStats;

    private List<Vector3> path = new();
    private int nextPathPoint = 0;

    private Coroutine slowCoroutine;

    public Action OnDeathAction;
    public Action<Enemy, TowerAttackTypes> OnDeathTowerAction;

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
        copyStats.CurrentMoveSpeed = copyStats.BaseMoveSpeed;
        nextPathPoint = 0;

        OnDeathAction += DropCurrency;
    }

    private void OnDisable()
    {
        OnDeathAction = null;
        OnDeathTowerAction = null;
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

        transform.position = Vector3.MoveTowards(transform.position, path[nextPathPoint], copyStats.CurrentMoveSpeed * Time.deltaTime);
    }

    private void CheckWayPoint()
    {
        float distanceSq = (path[nextPathPoint] - transform.position).sqrMagnitude;

        if (distanceSq < 0.001f)
        {
            nextPathPoint++;

            if (nextPathPoint >= path.Count)
            {
                GameManager.Instance.Damage(copyStats.Damage);
                HealthManager.Instance.GetDamage();
                OnDeathAction?.Invoke();
                OnDeathTowerAction?.Invoke(this, copyStats.EnemyTargetType);
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
            OnDeathAction?.Invoke();
            OnDeathTowerAction?.Invoke(this, copyStats.EnemyTargetType);
            EnemyPoolingManager.Instance.BackInPool(this.gameObject, copyStats.EnemyType);
            gameObject.SetActive(false);
        }
    }

    public void Slow(float slowStrength, float slowDuration)
    {
        copyStats.CurrentMoveSpeed *= ( 1 - slowStrength);

        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        slowCoroutine = StartCoroutine(SlowTime(slowDuration));
    }

    private IEnumerator SlowTime(float slowDuration)
    {
        yield return new WaitForSeconds(slowDuration);

        copyStats.CurrentMoveSpeed = copyStats.BaseMoveSpeed;

        slowCoroutine = null;
    }


    private void DropCurrency()
    {
        CurrencyManager.Instance.UpdateCurrencyOverlay(copyStats.CurrencyType, copyStats.CurrencyDropAmount);
    }
}
