using UnityEngine;

public class DeathImpuls : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnEnable()
    {
        if (enemy != null)
            enemy.OnDeathAction += EmittingTrail;
    }


    private void EmittingTrail()
    {
        trailRenderer.emitting = true;
    }

    private void OnDisable()
    {
        trailRenderer.emitting = false;
        trailRenderer.Clear();
    }
}
