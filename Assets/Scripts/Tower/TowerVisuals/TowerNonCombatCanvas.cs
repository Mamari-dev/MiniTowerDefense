using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerNonCombatCanvas : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerDescription;

    private TowerBaseStats towerBaseStats;
    private Vector2 startPos;

    private void Awake()
    {
        startPos = rectTransform.anchoredPosition;
    }

    protected virtual void Start()
    {
        Tower tower = GetComponentInParent<Tower>();
        if (tower == null) return;

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
        rectTransform.anchoredPosition = startPos;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldDelta = eventData.delta / Screen.height * Camera.main.orthographicSize * 2f;
        rectTransform.position += worldDelta;
    }
}
