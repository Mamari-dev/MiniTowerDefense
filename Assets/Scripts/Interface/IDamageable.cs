using UnityEngine;

public interface IDamageable
{
    public void Damage(float damage, Vector2 hitPoint, TowerDamageType damageType);
}
