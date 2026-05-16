using UnityEngine;

public class WaveManagerActionLineRenderer : WaveManagerAction
{
    [SerializeField] private LineRenderer lineRenderer;
    protected override void DisableObject()
    {
        lineRenderer.enabled = false;
    }

    protected override void EnableObject()
    {
        lineRenderer.enabled = true;
    }
}
