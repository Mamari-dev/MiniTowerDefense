using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileFreezeStats", menuName = "Scriptable Objects/ProjectileFreezeStats")]
public class ProjectileSlowStats : ProjectileStats
{
    [HideInInspector]public float SlowStrength;
    [HideInInspector]public float SlowDuration;
}
