using UnityEngine;

public class ProjectileSlow : Projectile, ISlowable
{
    private ProjectileSlowStats slowStats;

    protected override void Awake()
    {
        base.Awake();
        slowStats = (ProjectileSlowStats)runTimeStats;
    }

    public void Slow(float slowStrength, float slowDuration)
    {
        slowStats.SlowStrength = slowStrength;
        slowStats.SlowDuration = slowDuration;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == slowStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);

            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(slowStats.Damage, hitPoint, slowStats.DamageType);

            if (collision.TryGetComponent(out ISlowable slowable))
                slowable.Slow(slowStats.SlowStrength, slowStats.SlowDuration);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, slowStats.projectileType);
        }
    }
}
