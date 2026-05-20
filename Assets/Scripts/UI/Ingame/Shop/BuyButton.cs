using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerCost;
    [SerializeField] private Image towerImage;

    private TowerBaseStats currentStats;

    public void InitDatas(TowerBaseStats stats)
    {
        currentStats = stats;
        towerName.text = stats.towerType.ToString();
        towerCost.text = stats.cost.ToString();
        towerImage.sprite = stats.towerIcon;
        towerImage.color = stats.iconColor;
    }

    public void OnClick()
    {
        PlayerInput.Instance.GetTower(currentStats);
    }
}
