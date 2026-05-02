using UnityEngine;

public class Tower_SingleAttack : Tower
{
    protected override void Shoot(Transform frontEnemyTransform)
    {
        GameObject projectile = ProjectilePoolingManager.Instance.GetProjectile();

        projectile.transform.position = transform.position;
        projectile.SetActive(true);

        if (projectile.TryGetComponent(out IShootable shootable))
        {
            shootable.SetProjectileValues(towerRunTimeStats.projectileDamage, towerRunTimeStats.projectileSpeed, frontEnemyTransform);
        }
    }
}
