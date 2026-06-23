using UnityEngine;

[CreateAssetMenu(fileName = "ShotStatsBulletFreeze", menuName = "Scriptable Objects/Shot/ShotStatsBulletBounce")]
public class ShotStatsBulletBounce : ShotStatsBullet
{
    [SerializeField] private LayerMask enemyLayer;
    private int bounceAmount;
    private float bounceRange;

    public LayerMask EnemyLayer { get => enemyLayer; set => enemyLayer = value; }
    public int BounceAmount { get => bounceAmount; set => bounceAmount = value; }
    public float BounceRange { get => bounceRange; set => bounceRange = value; }
}
