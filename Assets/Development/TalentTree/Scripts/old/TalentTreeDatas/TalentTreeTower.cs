using UnityEngine;

[CreateAssetMenu(fileName = "TalentTreeTower", menuName = "Scriptable Objects/TalentTree/Tower")]
public class TalentTreeTower : ScriptableObject
{
    [SerializeField] private TowerTypes towerType;
    [SerializeField] private bool isUnlocked = false;
    [SerializeField] private TalentTreeTowerData[] datas;

    public TowerTypes TowerType { get => towerType;}
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }
    public TalentTreeTowerData[] Datas { get => datas; set => datas = value; }

    public void UnlockTower()
    {
        isUnlocked = true;
    }

    public void SetNewStat(TalentTreeTowerStatType type, float value)
    {
        for (int i = 0; i < Datas.Length; i++)
        {
            if (Datas[i].type == type)
            {
                Datas[i].value += value;
                return;
            }
        }
    }
}
