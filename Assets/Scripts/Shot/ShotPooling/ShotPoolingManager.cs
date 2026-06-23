using System.Collections.Generic;
using UnityEngine;

public class ShotPoolingManager : MonoBehaviour
{
    [SerializeField] private ShotPoolingStruct[] projectilePrefabs;

    public static ShotPoolingManager Instance { get; private set; }
    private Dictionary<ShotTypes, Stack<GameObject>> pools = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        InstantiatePools();
    }

    private void InstantiatePools()
    {
        foreach (ShotTypes type in System.Enum.GetValues(typeof(EnemyType)))
        {
            pools[type] = new Stack<GameObject>();
        }
    }

    public GameObject GetProjectile(ShotTypes type)
    {
        return CheckPoolForObject(type);
    }

    private GameObject CheckPoolForObject(ShotTypes type)
    {
        if (pools[type].Count == 0)
        {
            for (int i = 0; i < projectilePrefabs.Length; i++)
            {
                if (projectilePrefabs[i].projectileTypes == type)
                    return Instantiate(projectilePrefabs[i].projectilePrefab, transform);
            }

        }

        return pools[type].Pop();
    }

    public void BackInPool(GameObject poolObject, ShotTypes type)
    {
        pools[type].Push(poolObject);
        poolObject.SetActive(false);
    }
}
