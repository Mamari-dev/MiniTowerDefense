using UnityEngine;

public class AttackRangeVisual : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int segments;
    [SerializeField] private float thickness;

    private void Start()
    {
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
    }

    public void DrawAttackRange(float attackRange)
    {
        lineRenderer.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;
            lineRenderer.SetPosition(i, new Vector3
            (
                Mathf.Cos(angle) * attackRange / transform.localScale.x,
                Mathf.Sin(angle) * attackRange / transform.localScale.y,
                0f
            ));
        }
    }

    public void EnAndDisableRenderer()
    {
        lineRenderer.enabled = !lineRenderer.enabled;
    }
}
