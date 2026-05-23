using UnityEngine;

public class FreezeTower : TowerCombat
{
    private TowerCombatFreezeStats freezeStats;
    protected override void Awake()
    {
        base.Awake();

        freezeStats = (TowerCombatFreezeStats)towerRunTimeCombatStats;
    }

    protected override void Shoot(Transform frontEnemyTransform)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.projectileType);

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        IShootable shootable = null;
        ISlowable slowable = null;
        if (projectile.TryGetComponent(out shootable)
            && projectile.TryGetComponent(out slowable))
        {
            shootable.SetProjectileValues(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.projecttileSpeed, frontEnemyTransform);

            slowable.Slow(freezeStats.slowStrength, freezeStats.slowDuration);
        }
        else
        {
            Debug.Log(projectile.name);
            Debug.Log(shootable);
            Debug.Log(slowable);
        }
    }
}