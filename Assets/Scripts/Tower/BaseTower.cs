using UnityEngine;

public class BaseTower : TowerCombatShooting
{

    protected override void Shoot(Enemy enemy)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.projectileType);

        projectile.transform.position = transform.position;

        if (projectile.TryGetComponent(out IShootable shootable))
        {
            shootable.SetProjectileValues(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.projecttileSpeed, enemy);
        }
        projectile.SetActive(true);
    }
}