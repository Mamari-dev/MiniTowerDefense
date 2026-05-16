using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats_SO", menuName = "Scriptable Objects/TowerStats_SO")]
public class TowerStats : ScriptableObject
{
    [Header("Tower Values")]
    public string towerName;
    public Sprite icon;
    public Color iconColor;
    public CurrencyTypes currencyType;
    public int cost;

    [Header("Tower Combat Values")]
    public float attackRange;
    public float attackSpeed;
    public float attackDamage;
    public float projecttileSpeed;

    [Header("Tower Prefab")]
    public GameObject prefab;
    public GameObject ghostPrefab;
}
