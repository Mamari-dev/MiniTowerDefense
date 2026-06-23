using UnityEngine;

[CreateAssetMenu(fileName = "TowerCombatBounceStats", menuName = "Scriptable Objects/Tower/TowerCombatBounceStats")]
public class TowerCombatBounceStats : TowerCombatShootingStats
{
    [Header("Bounce Values")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceRange;

    public int BounceAmount { get => bounceAmount; set => bounceAmount = value; }
    public float BounceRange { get => bounceRange; set => bounceRange = value; }
}
