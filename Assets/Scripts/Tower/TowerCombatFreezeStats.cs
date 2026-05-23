using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatFreezeStats", menuName = "Scriptable Objects/TowerCombatFreezeStats")]
public class TowerCombatFreezeStats : TowerCombatStats
{
    [Header("SlowValues")]
    [Range(0,1)]public float slowStrength;
    public float slowDuration;
}
