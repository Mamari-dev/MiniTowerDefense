using UnityEngine;

public class BuyButtonSpawner : MonoBehaviour
{
    [SerializeField] private GameObject buyButtonPrefab;
    [SerializeField] private TowerBaseStats[] towerDatas;
    [SerializeField] private RectTransform shopTransform;

    private void Start()
    {
        for (int i = 0; i < towerDatas.Length; i++)
        {
            SpawnTower(towerDatas[i]);
        }
    }

    private void SpawnTower(TowerBaseStats stats)
    {
        GameObject button = Instantiate(buyButtonPrefab, shopTransform);
        BuyButton buyButton = button.GetComponent<BuyButton>();
        buyButton.InitDatas(stats);
    }
}
