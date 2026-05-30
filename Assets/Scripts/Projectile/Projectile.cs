using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    [SerializeField] private ProjectileStats projectileStats;
    protected ProjectileStats copyProjectileStats;

    protected virtual void Awake()
    {
        copyProjectileStats = Instantiate(projectileStats);
    }

    public void SetProjectileValues(float damage, float speed, Transform targetTransform)
    {
        copyProjectileStats.Damage = damage;
        copyProjectileStats.Speed = speed;
        copyProjectileStats.Target = targetTransform;
    }

    private void Update()
    {
        if (copyProjectileStats.Target != null && copyProjectileStats.Target.gameObject.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, copyProjectileStats.Target.position, Time.deltaTime * copyProjectileStats.Speed);
        else
            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, copyProjectileStats.projectileType);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == copyProjectileStats.Target)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(copyProjectileStats.Damage);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, copyProjectileStats.projectileType);
        }
    }
}
