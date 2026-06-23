using UnityEngine;

[CreateAssetMenu(fileName = "TowerBaseStats", menuName = "Scriptable Objects/Tower/TowerBaseStats")]
public class TowerBaseStats : ScriptableObject
{
    [Header("Tower Base Values")]
    public TowerTypes towerType;
    public Sprite towerIcon;
    public string towerDescription;
    public Color iconColor;
    public CurrencyTypes currencyType;
    public int cost;

    [Header("Tower Prefab")]
    public GameObject prefab;
    public GameObject ghostPrefab;
}
