using UnityEngine;

public class ShotBullet : Shot, IBulletShot
{
    protected ShotStatsBullet bulletStats;

    protected override void Awake()
    {
        base.Awake();
        bulletStats = (ShotStatsBullet)runTimeStats;
    }

    public void SetBulletValues(TowerDamageType damageType, float damage, Enemy target, float speed)
    {
        SetShotValues(damageType, damage);
        bulletStats.Target = target;
        bulletStats.Speed = speed;
    }

    private void Update()
    {
        if (bulletStats.Target != null && !bulletStats.Target.IsDead)
            transform.position = Vector2.MoveTowards(transform.position, bulletStats.Target.transform.position, Time.deltaTime * bulletStats.Speed);
        else
        {
            ShotPoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.ProjectileType);
            Debug.Log("Update Back in Pool");
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == bulletStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(runTimeStats.Damage, hitPoint, runTimeStats.DamageType);

            ShotPoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.ProjectileType);
        }
    }
}
