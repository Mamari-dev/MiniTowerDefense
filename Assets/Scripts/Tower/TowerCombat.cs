using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerCombat : Tower
{
    [SerializeField] private TowerCombatStats towerCombatStats;
    protected TowerCombatStats towerRunTimeCombatStats;
    [SerializeField] private CircleCollider2D towerCollider;
    [SerializeField] private AttackRangeVisual attackRangeVisual;

    [SerializeField] private LayerMask enemyLayer;
    private Dictionary<TowerAttackTypes, List<Enemy>> enemies = new();

    #region Editor
    public TowerCombatStats TowerCombatStats { get => towerCombatStats; }
    public TowerCombatStats TowerRunTimeCombatStats { get => towerRunTimeCombatStats; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        towerRunTimeCombatStats = Instantiate(towerCombatStats);
        towerCollider.radius = towerRunTimeCombatStats.attackRange;
        attackRangeVisual.DrawAttackRange(towerRunTimeCombatStats.attackRange);
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

    private void OnTriggerEnter2D(Collider2D collision)
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

            if (enemies[TowerAttackTypes.Health].Count == 1)
                StartCoroutine(AttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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

            if (enemies[TowerAttackTypes.Health].Count == 0)
                StopAllCoroutines();
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        while (enemies[TowerAttackTypes.Health].Count > 0)
        {
            List<Enemy> preferedList = GetAttackTargetList();
            List<Enemy> targetList = GetTargets(preferedList);

            for (int i = 0; i < targetList.Count; i++)
            {
                Transform targetTransform = targetList[i].transform;
                Shoot(targetTransform);
            }

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }
    }

    protected abstract void Shoot(Transform frontEnemyTransform);

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

    private List<Enemy> GetAttackTargetList()
    {
        if (enemies.TryGetValue(towerRunTimeCombatStats.attackTargetTypes, out List<Enemy> enemyList)
            && enemyList.Count >= towerRunTimeCombatStats.targetAmount)
            return enemyList;
        else
            return enemies[TowerAttackTypes.Health];
    }



    private List<Enemy> GetTargets(List<Enemy> enemyList)
    {
        List<Enemy> targets = new();
        int targetAmount = towerRunTimeCombatStats.targetAmount;
        if (enemyList.Count < targetAmount)
            targetAmount = enemyList.Count;

        switch (towerRunTimeCombatStats.attackPattern)
        {
            case TowerAttackPattern.Front:
                {
                    for (int i = 0; i < targetAmount; i++)
                    {
                        targets.Add(enemyList[i]);
                    }
                    return targets;
                }
            case TowerAttackPattern.Back:
                {
                    int startIndex = enemyList.Count - 1;
                    for (int i = 0; i < targetAmount; i++)
                    {
                        targets.Add(enemyList[startIndex - i]);
                    }
                    return targets;
                }
            case TowerAttackPattern.Highest:
                {
                    List<Enemy> sortedList = GetHighSortedTargetList(enemyList);
                    for (int i = 0; i < targetAmount; i++)
                    {
                        targets.Add(sortedList[i]);
                    }
                    return targets;
                }
            case TowerAttackPattern.Lowest:
                {
                    List<Enemy> sortedList = GetLowSortedTargetList(enemyList);
                    for (int i = 0; i < targetAmount; i++)
                    {
                        targets.Add(sortedList[i]);
                    }
                    return targets;
                }
            default:
                {
                    Debug.Log("AttackPattern Not Found!");
                    return enemyList;
                }
        }
    }

    private List<Enemy> GetHighSortedTargetList(List<Enemy> preferedList)
    {
        switch (towerRunTimeCombatStats.attackTargetTypes)
        {
            case TowerAttackTypes.Health:
                {
                    return preferedList.OrderByDescending(enemy => enemy.CopyStats.CurrentHealth).ToList();
                }
            case TowerAttackTypes.Speed:
                {
                    return preferedList.OrderByDescending(enemy => enemy.CopyStats.BaseMoveSpeed).ToList();
                }
            default:
                {
                    Debug.Log("AttackType Not Found!");
                    return preferedList;
                }
        }
    }

    private List<Enemy> GetLowSortedTargetList(List<Enemy> preferedList)
    {
        switch (towerRunTimeCombatStats.attackTargetTypes)
        {
            case TowerAttackTypes.Health:
                {
                    return preferedList.OrderBy(enemy => enemy.CopyStats.CurrentHealth).ToList();
                }
            case TowerAttackTypes.Speed:
                {
                    return preferedList.OrderBy(enemy => enemy.CopyStats.BaseMoveSpeed).ToList();
                }
            default:
                return preferedList;
        }
    }
}