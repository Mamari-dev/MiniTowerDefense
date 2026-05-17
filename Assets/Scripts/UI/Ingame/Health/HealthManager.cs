using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private Color heartColor;
    [SerializeField] private Color damagedHeartColor;

    private Stack<Image> heartImages = new();
    private Queue<Image> damagedHeartImages = new();

    public static HealthManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SpawnHealth(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Image image = Instantiate(healthPrefab, transform).GetComponent<Image>();
            heartImages.Push(image);
        }
    }

    public void AddHealth()
    {
        Image image = Instantiate(healthPrefab, transform).GetComponent<Image>();
        heartImages.Push(image);
    }

    public void GetDamage()
    {
        Image image = heartImages.Pop();
        image.color = damagedHeartColor;
        damagedHeartImages.Enqueue(image);
    }

    public void GetHeal()
    {
        Image image = damagedHeartImages.Dequeue();
        image.color = heartColor;
        heartImages.Push(image);
    }
}
