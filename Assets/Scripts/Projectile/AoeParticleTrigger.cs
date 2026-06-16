using System.Collections.Generic;
using UnityEngine;

public class AoeParticleTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private HashSet<GameObject> hittetEnemy = new();
    private float damageValue;
    private TowerDamageType damageType;

    public void Init(float damageValue, TowerDamageType damageType)
    {
        this.damageValue = damageValue;
        this.damageType = damageType;
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> hitParticle = new();
        int count = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, hitParticle, out ParticleSystem.ColliderData enterData);
        
        if (count == 0) return;

        int colliderCount = enterData.GetColliderCount(0);
        
        for (int i = 0; i < colliderCount; i++)
        {
            Collider2D col = enterData.GetCollider(0, i) as Collider2D;
            if (col == null) continue;

            GameObject hit = col.gameObject;
            if (hittetEnemy.Contains(hit)) continue;
            hittetEnemy.Add(hit);

            Vector2 hitPoint = col.ClosestPoint(transform.position);

            if (hit.TryGetComponent(out IDamageable damage))
                damage.Damage(damageValue, hitPoint, damageType);
        }
    }

    private void OnParticleSystemStopped()
    {
        hittetEnemy.Clear();
    }
}
