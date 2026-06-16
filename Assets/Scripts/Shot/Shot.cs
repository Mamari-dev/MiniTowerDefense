using UnityEngine;

public abstract class Shot : MonoBehaviour
{
    [SerializeField] private ShotStats baseStats;
    protected ShotStats runTimeStats;

    public ShotStats BaseStats { get => baseStats; set => baseStats = value; }
    public ShotStats RunTimeStats { get => runTimeStats; set => runTimeStats = value; }
    [HideInInspector] public bool foldout;

    protected virtual void Awake()
    {
        runTimeStats = Instantiate(baseStats);
    }

    protected virtual void SetShotValues(TowerDamageType damageType, float damage)
    {
        runTimeStats.DamageType = damageType;
        runTimeStats.Damage = damage;
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
