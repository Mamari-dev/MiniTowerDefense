using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject towerButtonPrefab;
    [SerializeField] private RectTransform shopRectTransform;
    [SerializeField] private float speed;

    private int openPosition = 0;
    private float closePosition;
    private bool isShopOpen = false;
    private Coroutine moveCoroutine;

    private void Start()
    {
        closePosition = shopRectTransform.rect.width;
        WaveManager.StartWave += CloseShop;
    }

    private void CloseShop()
    {
        if (isShopOpen == true) return;

        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        moveCoroutine = StartCoroutine(MoveShopWindow(closePosition));
        isShopOpen = false;
    }

    private void SpawnTowerButton(TowerBaseStats stats)
    {
        GameObject button = Instantiate(towerButtonPrefab, shopRectTransform);
        BuyButton buyButton = button.GetComponent<BuyButton>();
        buyButton.InitDatas(stats);
    }

    public void OnShopButtonClick()
    {
        if (moveCoroutine != null)
            StopCoroutine(moveCoroutine);

        if (isShopOpen)
        {
            moveCoroutine = StartCoroutine(MoveShopWindow(closePosition));
            isShopOpen = false;
        }
        else
        {
            moveCoroutine = StartCoroutine(MoveShopWindow(openPosition));
            isShopOpen = true;
        }
    }

    private IEnumerator MoveShopWindow(float goalPosition)
    {
        yield return new WaitUntil(() =>
        {
            Vector2 newPos = new(Mathf.MoveTowards(shopRectTransform.anchoredPosition.x, goalPosition, speed * Time.deltaTime), shopRectTransform.anchoredPosition.y);
            shopRectTransform.anchoredPosition = newPos;

            return Mathf.Approximately(shopRectTransform.anchoredPosition.x, goalPosition);
        });

        moveCoroutine = null;
    }



    #region später wavemanager
    [SerializeField] private TowerBaseStats tower;

    [ContextMenu("spawn tower button")]
    private void SpawnTower()
    {
        SpawnTowerButton(tower);
    }
    #endregion
}
