using System.Collections;
using UnityEngine;


public class AoeTower : TowerCombatPulsing
{
    protected override IEnumerator AttackCoroutine()
    {
        while (enemies[TowerAttackTypes.Health].Count > 0)
        {
            GameObject shot = ShotPoolingManager.Instance.GetProjectile(towerRunTimeCombatStats.ProjectileType);
            shot.transform.position = transform.position;

            if (shot.TryGetComponent(out IParticleShot particleShot))
            {
                particleShot.SetParticleValues(towerRunTimeCombatStats.damageType, towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.attackRange);
            }
            shot.SetActive(true);

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }

        attackCoroutine = null;
    }
}
