using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    [SerializeField] private ProjectileStats baseStats;
    protected ProjectileStats runTimeStats;

    protected virtual void Awake()
    {
        runTimeStats = Instantiate(baseStats);
    }

    public void SetProjectileValues(float damage, float speed, Enemy target, TowerDamageType damageType)
    {
        runTimeStats.Damage = damage;
        runTimeStats.Speed = speed;
        runTimeStats.Target = target;
        runTimeStats.DamageType = damageType;
    }

    private void Update()
    {
        if (runTimeStats.Target != null && !runTimeStats.Target.IsDead)
            transform.position = Vector2.MoveTowards(transform.position, runTimeStats.Target.transform.position, Time.deltaTime * runTimeStats.Speed);
        else
            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.projectileType);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == runTimeStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(runTimeStats.Damage, hitPoint, runTimeStats.DamageType);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, runTimeStats.projectileType);
        }
    }
}
