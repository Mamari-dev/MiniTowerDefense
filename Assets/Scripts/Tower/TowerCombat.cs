using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerCombat : Tower
{
    [SerializeField] protected CircleCollider2D towerCollider;
    [SerializeField] protected AttackRangeVisual attackRangeVisual;
    public CircleCollider2D TowerCollider { get => towerCollider; private set => towerCollider = value; }

    [SerializeField] private LayerMask enemyLayer;
    protected Dictionary<TowerAttackTypes, List<Enemy>> enemies = new();
    protected Coroutine attackCoroutine = null;


    protected override void Awake()
    {
        base.Awake();
        attackRangeVisual.EnAndDisableRenderer();

        InitDictionay();
    }

    private void InitDictionay()
    {
        foreach (TowerAttackTypes types in Enum.GetValues(typeof(TowerAttackTypes)))
        {
            enemies.Add(types, new List<Enemy>());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            TowerAttackTypes enemyFlags = enemy.CopyStats.EnemyTargetType;

            foreach (TowerAttackTypes type in enemies.Keys)
            {
                if (enemyFlags.HasFlag(type))
                    enemies[type].Add(enemy);

            }
            enemy.OnDeathTowerAction += RemoveDeadEnemy;

            if (enemies[TowerAttackTypes.Health].Count == 1 && attackCoroutine == null)
                attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0 && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            TowerAttackTypes enemyFlags = enemy.CopyStats.EnemyTargetType;

            foreach (TowerAttackTypes type in enemies.Keys)
            {
                if (enemyFlags.HasFlag(type))
                    enemies[type].Remove(enemy);
            }
            enemy.OnDeathTowerAction -= RemoveDeadEnemy;

            //if (enemies[TowerAttackTypes.Health].Count == 0)
            //    StopAllCoroutines();
        }
    }

    protected abstract IEnumerator AttackCoroutine();

    /// <summary>
    /// safety method, sometimes OnExit Triggers not when enemy get disabled
    /// </summary>
    private void RemoveDeadEnemy(Enemy enemy, TowerAttackTypes enemyFlags)
    {
        foreach (TowerAttackTypes type in enemies.Keys)
        {
            if (enemyFlags.HasFlag(type) && enemies[type].Contains(enemy))
                enemies[type].Remove(enemy);
        }

        if (enemies[TowerAttackTypes.Health].Count == 0)
            StopAllCoroutines();
    }

    public abstract float GetAttackRange();
}