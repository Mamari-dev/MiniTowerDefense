using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    [SerializeField] private ProjectileStats projectileStats;
    protected ProjectileStats copyProjectileStats;

    protected virtual void Awake()
    {
        copyProjectileStats = Instantiate(projectileStats);
    }

    public void SetProjectileValues(float damage, float speed, Enemy target)
    {
        copyProjectileStats.Damage = damage;
        copyProjectileStats.Speed = speed;
        copyProjectileStats.Target = target;
    }

    private void Update()
    {
        if (copyProjectileStats.Target != null && !copyProjectileStats.Target.IsDead)
            transform.position = Vector2.MoveTowards(transform.position, copyProjectileStats.Target.transform.position, Time.deltaTime * copyProjectileStats.Speed);
        else
            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, copyProjectileStats.projectileType);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == copyProjectileStats.Target.transform)
        {
            Vector2 hitPoint = collision.ClosestPoint(transform.position);
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(copyProjectileStats.Damage, hitPoint);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, copyProjectileStats.projectileType);
        }
    }
}
