using UnityEngine;

public class Projectile : MonoBehaviour, IShootable
{
    private float damage;
    private float speed;
    private Transform target;

    public void SetProjectileValues(float damage, float speed, Transform targetTransform)
    {
        this.damage = damage;
        this.speed = speed;
        target = targetTransform;
    }

    private void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        else
            ProjectilePoolingManager.Instance.BackInPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
                damageable.Damage(damage);

            ProjectilePoolingManager.Instance.BackInPool(this.gameObject);
        }
    }
}
