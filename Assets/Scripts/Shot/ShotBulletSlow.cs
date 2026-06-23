using UnityEngine;

public class ShotBulletSlow : ShotBullet, IBulletSlowShot
{
    private ShotStatsBulletSlow slowStats;

    protected override void Awake()
    {
        base.Awake();
        slowStats = (ShotStatsBulletSlow)runTimeStats;
    }

    public void Slow(float slowStrength, float slowDuration)
    {
        slowStats.SlowStrength = slowStrength;
        slowStats.SlowDuration = slowDuration;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == bulletStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);

            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(slowStats.Damage, hitPoint, slowStats.DamageType);

            if (collision.TryGetComponent(out ISlowable slowable))
                slowable.Slow(slowStats.SlowStrength, slowStats.SlowDuration);

            ShotPoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.ProjectileType);
        }
    }
}
