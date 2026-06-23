using UnityEngine;

[CreateAssetMenu(fileName = "ShotStatsParticle", menuName = "Scriptable Objects/Shot/ShotStatsParticle")]
public class ShotStatsParticle : ShotStats
{
    [SerializeField] private float ringSizeMultiplier;
    private float attackRange;
    private float particleRange;

    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float RingSizeMultiplier { get => ringSizeMultiplier; set => ringSizeMultiplier = value; }
    public float ParticleRange { get => particleRange; set => particleRange = value; }
}
