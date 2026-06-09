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
            trigger.Init(towerRunTimeCombatStats.attackDamage);
    }

    protected override IEnumerator AttackCoroutine()
    {
        var psMain = pS.main;
        psMain.startSize = new ParticleSystem.MinMaxCurve(towerRunTimeCombatStats.attackRange * 2);

        while (true)
        {
            trigger.HittetEnemy.Clear();
            pS.Play();

            yield return new WaitForSeconds(towerRunTimeCombatStats.attackSpeed);
        }
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
