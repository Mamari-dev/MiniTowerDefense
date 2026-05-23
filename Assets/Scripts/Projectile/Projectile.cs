using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    [SerializeField] protected ProjectileStats projectileStats;

    public void SetProjectileValues(float damage, float speed, Transform targetTransform)
    {
        projectileStats.Damage = damage;
        projectileStats.Speed = speed;
        projectileStats.Target = targetTransform;
    }

    private void Update()
    {
        if (projectileStats.Target != null && projectileStats.Target.gameObject.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, projectileStats.Target.position, Time.deltaTime * projectileStats.Speed);
        else
            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, projectileStats.projectileType);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == projectileStats.Target)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(projectileStats.Damage);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject, projectileStats.projectileType);
        }
    }
}
