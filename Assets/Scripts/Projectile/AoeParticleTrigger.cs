using System.Collections.Generic;
using UnityEngine;

public class AoeParticleTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    private HashSet<GameObject> hittetEnemy = new();
    private float damageValue;

    public HashSet<GameObject> HittetEnemy { get => hittetEnemy; set => hittetEnemy = value; }

    public void Init(float damageValue)
    {
        this.damageValue = damageValue;
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> hitEnemy = new();
        ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, hitEnemy, out ParticleSystem.ColliderData enterData);

        for (int i = 0; enterData.GetCollider(i, 0); i++)
        {
            GameObject hit = enterData.GetCollider(i, 0).gameObject;

            if (hittetEnemy.Contains(hit)) continue;
            hittetEnemy.Add(hit);

            Collider2D enemyCollider = enterData.GetCollider(i, 0) as Collider2D;
            Vector2 hitPoint = enemyCollider.ClosestPoint(transform.position);

            if (hit.TryGetComponent(out IDamageable damage))
                damage.Damage(damageValue, hitPoint);
        }
    }
}
