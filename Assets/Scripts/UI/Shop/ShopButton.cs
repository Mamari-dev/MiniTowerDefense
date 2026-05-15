using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ShopButton : MonoBehaviour
{
    private int openPosition = 0;
    private float closePosition;
    private bool isShopOpen = false;

    [SerializeField] private float speed;

    private RectTransform rectTransform;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        closePosition = rectTransform.rect.width;
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
                Vector2 newPos = new(Mathf.MoveTowards(rectTransform.anchoredPosition.x, goalPosition, speed * Time.deltaTime), rectTransform.anchoredPosition.y);
                rectTransform.anchoredPosition = newPos;

                return Mathf.Approximately(rectTransform.anchoredPosition.x, goalPosition);
            });

        moveCoroutine = null;
    }
}
