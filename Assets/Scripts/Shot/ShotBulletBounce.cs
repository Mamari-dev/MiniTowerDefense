using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShotBulletBounce : ShotBullet, IBulletBounceShot
{
    private ShotStatsBulletBounce bounceStats;
    private int hittet = 0;
    private List<Collider2D> hittetEnemys = new();

    protected override void Awake()
    {
        base.Awake();
        bounceStats = (ShotStatsBulletBounce)runTimeStats;
    }

    public void Bounce(int bounceAmount, float bounceRange)
    {
        bounceStats.BounceAmount = bounceAmount;
        bounceStats.BounceRange = bounceRange;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == bulletStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);

            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(bounceStats.Damage, hitPoint, bounceStats.DamageType);

            hittet++;
            hittetEnemys.Add(collision);

            if (hittet == bounceStats.BounceAmount || !FindNextTarget(hitPoint, out Enemy newTarget))
                ShotPoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.ProjectileType);
            else
                bulletStats.Target = newTarget;
        }
    }

    private bool FindNextTarget(Vector2 hitPoint, out Enemy newTarget)
    {
        List<Collider2D> colliderInRanges = Physics2D.OverlapCircleAll(hitPoint, bounceStats.BounceRange, bounceStats.EnemyLayer).ToList();

        foreach (Collider2D collider in hittetEnemys)
        {
            if (colliderInRanges.Contains(collider))
                colliderInRanges.Remove(collider);
        }

        if (colliderInRanges.Count == 0)
        {
            newTarget = null;
            return false;
        }

        Collider2D bestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliderInRanges)
        {
            float distance = Vector2.Distance(hitPoint, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = collider;
            }
        }

        if (bestTarget.TryGetComponent(out Enemy enemy))
            newTarget = enemy;
        else
        {
            newTarget = null;
            return false;
        }

        return true;
    }

    private void OnDisable()
    {
        hittet = 0;
        hittetEnemys.Clear();
    }
}
