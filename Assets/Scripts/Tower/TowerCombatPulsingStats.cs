using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatPulsingStats", menuName = "Scriptable Objects/TowerCombatPulsingStats")]
public class TowerCombatPulsingStats : TowerCombatStats
{
    [SerializeField] private ProjectileTypes projectileType;

    public ProjectileTypes ProjectileType { get => projectileType; set => projectileType = value; }
}
