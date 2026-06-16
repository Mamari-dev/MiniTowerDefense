using System.Collections;
using UnityEngine;


public class AoeTower : TowerCombatPulsing
{
    [SerializeField] private ParticleSystem pS;
    private AoeParticleTrigger trigger;

    protected override void Awake()
    {
        base.Awake();

        trigger = GetComponentInChildren<AoeParticleTrigger>();
        if (trigger != null)
            trigger.Init(towerRunTimeCombatStats.attackDamage, towerRunTimeCombatStats.damageType);
    }

    protected override IEnumerator AttackCoroutine()
    {
        var psMain = pS.main;
        psMain.startSize = new ParticleSystem.MinMaxCurve(towerRunTimeCombatStats.attackRange * 2);

        while (enemies[TowerAttackTypes.Health].Count == 1)
        {
            pS.Play();

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }

        attackCoroutine = null;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        pS.trigger.AddCollider(collision);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        pS.trigger.RemoveCollider(collision);
    }
}
