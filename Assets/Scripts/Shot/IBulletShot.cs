using UnityEngine;

public interface IBulletShot
{
    public void SetBulletValues(TowerDamageType damageType, float damage, Enemy target, float speed);
}
