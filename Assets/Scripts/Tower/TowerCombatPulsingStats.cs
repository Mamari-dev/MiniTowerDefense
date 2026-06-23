using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatPulsingStats", menuName = "Scriptable Objects/Tower/TowerCombatPulsingStats")]
public class TowerCombatPulsingStats : TowerCombatStats
{
    [SerializeField] private ShotTypes projectileType;

    public ShotTypes ProjectileType { get => projectileType; set => projectileType = value; }
}
