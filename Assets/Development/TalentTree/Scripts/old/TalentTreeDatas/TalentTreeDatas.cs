using System.Linq;
using UnityEngine;

public class TalentTreeDatas : MonoBehaviour
{
    [SerializeField] private TalentTreeTower[] towerDatas;

    private static TalentTreeDatas instance;
    public static TalentTreeDatas Instance { get => instance; }

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
    }

    public void Upgrade(TowerTypes towerType, TalentTreeTowerStatType statType, float value)
    {
        TalentTreeTower targetData = towerDatas.FirstOrDefault(t => t.TowerType == towerType);
        if (targetData == null) return;

        TalentTreeTowerData statData = targetData.Datas.FirstOrDefault(s => s.type == statType);
        statData.value += value;
    }
}
