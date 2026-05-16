using UnityEngine;

[CreateAssetMenu(fileName = "TowerBaseStats", menuName = "Scriptable Objects/TowerBaseStats")]
public class TowerBaseStats : ScriptableObject
{
    [Header("Tower Base Values")]
    public string towerName;
    public Sprite icon;
    public Color iconColor;
    public CurrencyTypes currencyType;
    public int cost;

    [Header("Tower Prefab")]
    public GameObject prefab;
    public GameObject ghostPrefab;
}
