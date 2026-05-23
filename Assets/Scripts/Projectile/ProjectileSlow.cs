using UnityEngine;

public class ProjectileSlow : Projectile, ISlowable
{
    private ProjectileSlowStats slowStats;

    private void Awake()
    {
        slowStats = (ProjectileSlowStats)projectileStats;
    }

    public void Slow(float slowStrength, float slowDuration)
    {
        slowStats.SlowStrength = slowStrength;
        slowStats.SlowDuration = slowDuration;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == slowStats.Target)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(slowStats.Damage);

            if (collision.TryGetComponent(out ISlowable slowable))
                slowable.Slow(slowStats.SlowStrength, slowStats.SlowDuration);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, slowStats.projectileType);
        }
    }
}
