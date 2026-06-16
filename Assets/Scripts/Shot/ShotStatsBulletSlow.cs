using UnityEngine;

[CreateAssetMenu(fileName = "ShotStatsBulletFreeze", menuName = "Scriptable Objects/Shot/ShotStatsBulletFreeze")]
public class ShotStatsBulletSlow : ShotStatsBullet
{
    private float slowStrength;
    private float slowDuration;

    public float SlowStrength { get => slowStrength; set => slowStrength = value; }
    public float SlowDuration { get => slowDuration; set => slowDuration = value; }
}
