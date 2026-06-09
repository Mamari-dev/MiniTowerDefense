using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class TowerCombatShooting : TowerCombat
{
    [SerializeField] private TowerCombatShootingStats towerCombatStats;
    protected TowerCombatShootingStats towerRunTimeCombatStats;

    #region Editor
    public TowerCombatShootingStats TowerCombatStats { get => towerCombatStats; }
    public TowerCombatShootingStats TowerRunTimeCombatStats { get => towerRunTimeCombatStats; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        towerRunTimeCombatStats = Instantiate(towerCombatStats);
        towerCollider.radius = towerRunTimeCombatStats.attackRange;
        attackRangeVisual.DrawAttackRange(towerRunTimeCombatStats.attackRange);
    }

    protected override IEnumerator AttackCoroutine()
    {
        while (enemies[TowerAttackTypes.Health].Count > 0)
        {
            List<Enemy> preferedList = GetAttackTargetList();
            List<Enemy> targetList = GetTargets(preferedList);

            for (int i = 0; i < targetList.Count; i++)
            {
                Shoot(targetList[i]);
            }

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }
    }

    protected abstract void Shoot(Enemy enemy);

    #region ListenOrdnung

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
                    return preferedList.OrderByDescending(enemy => enemy.CopyStats.Health).ToList();
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
                    return preferedList.OrderBy(enemy => enemy.CopyStats.Health).ToList();
                }
            case TowerAttackTypes.Speed:
                {
                    return preferedList.OrderBy(enemy => enemy.CopyStats.BaseMoveSpeed).ToList();
                }
            default:
                return preferedList;
        }
    }

#endregion

    public override float GetAttackRange()
    {
        return towerCombatStats.attackRange;
    }
}
