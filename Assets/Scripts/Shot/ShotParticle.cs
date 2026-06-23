using System.Collections.Generic;
using UnityEngine;

public class ShotParticle : Shot, IParticleShot
{
    private ShotStatsParticle particleStats;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private ParticleSystemRenderer psRenderer;
    private HashSet<GameObject> hittetEnemy = new();
    [SerializeField] private CircleCollider2D col;
    private ParticleSystem.Particle[] currentParticle;

    protected override void Awake()
    {
        base.Awake();
        particleStats = (ShotStatsParticle)runTimeStats;
    }

    public void SetParticleValues(TowerDamageType damageType, float damage, float attackRange)
    {
        SetShotValues(damageType, damage);
        particleStats.AttackRange = attackRange;
        particleStats.ParticleRange = attackRange * 2;

        col.radius = particleStats.AttackRange;
        var psMain = ps.main;
        psMain.startSize = new ParticleSystem.MinMaxCurve(particleStats.ParticleRange);
    }

    private void OnEnable()
    {
        ps.Play();
    }

    private void OnDisable()
    {
        currentParticle = null;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        ps.trigger.AddCollider(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ps.trigger.RemoveCollider(collision);
    }

    private void Update()
    {
        if (ps.particleCount == 0) return;

        if (currentParticle == null)
            currentParticle = new ParticleSystem.Particle[ps.particleCount];

        ps.GetParticles(currentParticle);

        for (int i = 0; i < currentParticle.Length; i++)
        {
            float currentRadius = particleStats.RingSizeMultiplier / currentParticle[i].GetCurrentSize(ps);
            float ringWidth = psRenderer.material.GetFloat("_RingWidth");
            psRenderer.material.SetFloat("_Radius", ringWidth - currentRadius);
        }
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> hitParticle = new();
        int count = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, hitParticle, out ParticleSystem.ColliderData enterData);

        if (count == 0) return;

        int colliderCount = enterData.GetColliderCount(0);

        for (int i = 0; i < colliderCount; i++)
        {
            Collider2D col = enterData.GetCollider(0, i) as Collider2D;
            if (col == null) continue;

            GameObject hit = col.gameObject;
            if (hittetEnemy.Contains(hit)) continue;
            hittetEnemy.Add(hit);

            Vector2 hitPoint = col.ClosestPoint(transform.position);

            if (hit.TryGetComponent(out IDamageable damage))
                damage.Damage(particleStats.Damage, hitPoint, particleStats.DamageType);
        }
    }

    private void OnParticleSystemStopped()
    {
        hittetEnemy.Clear();
        ShotPoolingManager.Instance.BackInPool(this.gameObject, particleStats.ProjectileType);
    }
}
