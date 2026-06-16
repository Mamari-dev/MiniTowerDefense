using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable, ISlowable
{
    [SerializeField] private EnemyStats baseStats;
    private EnemyStats copyStats;
    private Rigidbody2D rb;

    private List<Vector3> path;
    private int nextPathPoint = 0;
    private bool isDead = false;
    public bool IsDead { get => isDead; private set => isDead = value; }

    private Coroutine slowCoroutine;

    public Action OnDeathAction;                                //for WaveManager to Check when wave ends
    public Action<Enemy, TowerAttackTypes> OnDeathTowerAction;  //for Tower Combat Script (OnTrigger)

    #region Editor
    public EnemyStats BaseStats { get => baseStats; private set => baseStats = value; }
    public EnemyStats CopyStats { get => copyStats; set => copyStats = value; }

    [HideInInspector] public bool foldout;
    #endregion

    private void Awake()
    {
        copyStats = Instantiate(baseStats);
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isDead = false;
        path = new(MapManager.Instance.CurrentWorldPath);
        copyStats.CurrentMoveSpeed = copyStats.BaseMoveSpeed;

        OnDeathAction += DropCurrency;
    }

    private void OnDisable()
    {
        path.Clear();
        nextPathPoint = 0;
        OnDeathAction = null;
        OnDeathTowerAction = null;
        rb.linearVelocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);
    }

    public void InitStats(float hpScalingValue, int waveCount)
    {
        float newHealth = baseStats.Health * Mathf.Pow(hpScalingValue, waveCount - 1);
        copyStats.Health = newHealth;
        copyStats.CurrentMoveSpeed = copyStats.BaseMoveSpeed;
    }

    private void Update()
    {
        if (isDead || nextPathPoint >= path.Count) return;

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
                isDead = true;
                GameManager.Instance.Damage(copyStats.Damage);
                HealthManager.Instance.GetDamage();
                OnDeathAction?.Invoke();
                OnDeathTowerAction?.Invoke(this, copyStats.EnemyTargetType);
                EnemyPoolingManager.Instance.BackInPool(this.gameObject, copyStats.EnemyType);
            }
        }
    }

    public void Damage(float damage, Vector2 hitPoint, TowerDamageType damageType)
    {
        if (isDead) return;

        if (copyStats.Armored && damageType == TowerDamageType.armored)
            damage *= copyStats.ArmoredMultiplier;

        copyStats.Health -= damage;

        if (copyStats.Health <= 0)
        {
            isDead = true;
            OnDeathAction?.Invoke();
            OnDeathTowerAction?.Invoke(this, copyStats.EnemyTargetType);

            Vector2 knockbackDir = (Vector2)transform.position - hitPoint;
            rb.linearVelocity = knockbackDir.normalized * copyStats.DeathImpulsForce;

            StartCoroutine(DeathImpulse());
        }
    }

    private IEnumerator DeathImpulse()
    {
        yield return new WaitForSeconds(copyStats.DeathImpulseTime);

        EnemyPoolingManager.Instance.BackInPool(this.gameObject, copyStats.EnemyType);
    }

    public void Slow(float slowStrength, float slowDuration)
    {
        if (copyStats.CurrentMoveSpeed == copyStats.BaseMoveSpeed)
            copyStats.CurrentMoveSpeed *= (1 - slowStrength);

        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        if (this.gameObject.activeSelf)
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
