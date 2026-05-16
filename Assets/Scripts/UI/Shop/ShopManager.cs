using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject towerButtonPrefab;
    [SerializeField] private Transform parent;

    private void SpawnTowerButton(TowerStats stats)
    {
        GameObject button = Instantiate(towerButtonPrefab, parent);
        BuyButton buyButton = button.GetComponent<BuyButton>();
        buyButton.InitDatas(stats);
    }


    #region später wavemanager
    [SerializeField] private TowerStats tower;

    [ContextMenu("spawn tower button")]
    private void SpawnTower()
    {
        SpawnTowerButton(tower);
    }
    #endregion
}
