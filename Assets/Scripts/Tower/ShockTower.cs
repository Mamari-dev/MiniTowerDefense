using UnityEngine;

public class ShockTower : TowerCombatShooting
{
    private TowerCombatBounceStats bounceStats;
    protected override void Awake()
    {
        base.Awake();

        bounceStats = (TowerCombatBounceStats)towerRunTimeCombatStats;
    }

    protected override void Shoot(Enemy enemy)
    {
        GameObject shot = ShotPoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.shotType);

        shot.transform.position = transform.position;

        if (shot.TryGetComponent(out IBulletShot bulletShot))
        {
            bulletShot.SetBulletValues(towerRunTimeCombatStats.damageType, towerRunTimeCombatStats.attackDamage, enemy, towerRunTimeCombatStats.shotSpeed);
        }

        if (shot.TryGetComponent(out IBulletBounceShot bulletBounceShot))
        {
            bulletBounceShot.Bounce(bounceStats.BounceAmount, bounceStats.BounceRange);
        }

        shot.SetActive(true);
    }
}
