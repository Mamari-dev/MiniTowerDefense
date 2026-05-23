using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Scriptable Objects/ProjectileStats")]
public class ProjectileStats : ScriptableObject
{
    [SerializeField] public ProjectileTypes projectileType;
    [HideInInspector] public float Damage;
    [HideInInspector] public float Speed;
    [HideInInspector] public Transform Target;
}
