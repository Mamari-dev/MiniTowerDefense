using UnityEngine;

public class BaseTower : TowerCombat
{

    protected override void Shoot(Transform frontEnemyTransform)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.projectileType);

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        if (projectile.TryGetComponent(out IShootable shootable))
        {
            shootable.SetProjectileValues(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.projecttileSpeed, frontEnemyTransform);
        }
    }
}