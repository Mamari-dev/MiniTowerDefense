using UnityEngine;

public abstract class WaveManagerAction : MonoBehaviour
{
    private void Start()
    {
        WaveManager.StartWave += DisableObject;
        WaveManager.EndWave += EnableObject;
    }

    protected abstract void DisableObject();

    protected abstract void EnableObject();
}
