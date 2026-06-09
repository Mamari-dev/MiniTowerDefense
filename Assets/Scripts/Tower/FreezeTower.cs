using UnityEngine;

public class FreezeTower : TowerCombatShooting
{
    private TowerCombatFreezeStats freezeStats;
    protected override void Awake()
    {
        base.Awake();

        freezeStats = (TowerCombatFreezeStats)towerRunTimeCombatStats;
    }

    protected override void Shoot(Enemy enemy)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.projectileType);

        projectile.transform.position = transform.position;

        if (projectile.TryGetComponent(out IShootable shootable)
            && projectile.TryGetComponent(out ISlowable slowable))
        {
            shootable.SetProjectileValues(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.projecttileSpeed, enemy);

            slowable.Slow(freezeStats.slowStrength, freezeStats.slowDuration);
        }

        projectile.SetActive(true);
    }
}