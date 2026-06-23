using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatFreezeStats", menuName = "Scriptable Objects/Tower/TowerCombatFreezeStats")]
public class TowerCombatFreezeStats : TowerCombatShootingStats
{
    [Header("SlowValues")]
    [Range(0,1)]public float slowStrength;
    public float slowDuration;
}
