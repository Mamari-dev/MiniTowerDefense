using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TowerCombat : Tower
{
    [SerializeField] private TowerCombatStats towerCombatStats;
    protected TowerCombatStats towerRunTimeCombatStats;
    [SerializeField] private CircleCollider2D towerCollider;
    [SerializeField] private AttackRangeVisual attackRangeVisual;

    [SerializeField] private LayerMask enemyLayer;
    private List<Transform> enemys = new();

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            enemys.Add(collision.transform);

            if (enemys.Count == 1)
                StartCoroutine(AttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            enemys.Remove(collision.transform);

            if (enemys.Count == 0)
                StopAllCoroutines();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (enemys.Count > 0)
        {
            Transform frontEnemyTransform = enemys[0];
            Shoot(frontEnemyTransform);

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }
    }

    private void Shoot(Transform frontEnemyTransform)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile();

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        if (projectile.TryGetComponent(out IShootable shootable))
        {
            shootable.SetProjectileValues(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.projecttileSpeed, frontEnemyTransform);
        }
    }
}