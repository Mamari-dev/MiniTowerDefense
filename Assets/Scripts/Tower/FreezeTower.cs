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
        GameObject shot = ShotPoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.shotType);

        shot.transform.position = transform.position;

        if (shot.TryGetComponent(out IBulletShot bulletShot)
            && shot.TryGetComponent(out IBulletSlowShot bulletFreezeShot))
        {
            bulletShot.SetBulletValues(towerRunTimeCombatStats.damageType, towerRunTimeCombatStats.attackDamage, enemy, towerRunTimeCombatStats.shotSpeed);

            bulletFreezeShot.Slow(freezeStats.slowStrength, freezeStats.slowDuration);
        }

        shot.SetActive(true);
    }
}