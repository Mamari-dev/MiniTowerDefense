using TMPro;
using UnityEngine;

public class TowerNonCombatCanvas : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerDescription;

    private TowerBaseStats towerBaseStats;

    protected virtual void Start()
    {
        towerBaseStats = GetComponentInParent<Tower>().TowerRunTimeBaseStats;

        UpdateBaseStats();
        EnAndDisableCanvas();
    }

    private void UpdateBaseStats()
    {
        towerName.text = towerBaseStats.towerType.ToString();
        towerDescription.text = towerBaseStats.towerDescription;
    }

    public void EnAndDisableCanvas()
    {
        canvas.enabled = !canvas.enabled;
    }
}
