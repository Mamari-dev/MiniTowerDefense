using UnityEngine;

[CreateAssetMenu(fileName = "ShotStatsBullet", menuName = "Scriptable Objects/Shot/ShotStatsBullet")]
public class ShotStatsBullet : ShotStats
{
    private float speed;
    private Enemy target;

    public float Speed { get => speed; set => speed = value; }
    public Enemy Target { get => target; set => target = value; }
}
