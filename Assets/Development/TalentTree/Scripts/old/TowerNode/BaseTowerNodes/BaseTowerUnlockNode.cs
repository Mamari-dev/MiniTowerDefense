using UnityEngine;

public class BaseTowerAmountUpgradeNode : TalentUpgradeNode
{
    [SerializeField] private TowerTypes towerType;
    [SerializeField] private TalentTreeTowerStatType statType;
    [SerializeField] private float value;

    protected override void Upgrade()
    {
        TalentTreeDatas.Instance.Upgrade(towerType, statType, value);
    }
}
