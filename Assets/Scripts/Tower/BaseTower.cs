using UnityEngine;

public class BaseTower : TowerCombatShooting
{

    protected override void Shoot(Enemy enemy)
    {
        GameObject shot = ProjectilePoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.projectileType);

        shot.transform.position = transform.position;

        if (shot.TryGetComponent(out IBulletShot bulletShot))
        {
            bulletShot.SetBulletValues(towerRunTimeCombatStats.damageType, towerRunTimeCombatStats.attackDamage, enemy, towerRunTimeCombatStats.projecttileSpeed);
        }
        shot.SetActive(true);
    }
}