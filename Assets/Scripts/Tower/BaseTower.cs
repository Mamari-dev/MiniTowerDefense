using UnityEngine;

public class BaseTower : Tower
{
    protected override void Shoot(Transform frontEnemyTransform)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile();

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        if (projectile.TryGetComponent(out IShootable shootable))
        {
            shootable.SetProjectileValues(towerRunTimeStats.attackDamage, towerRunTimeStats.projecttileSpeed, frontEnemyTransform);
        }
    }
}
