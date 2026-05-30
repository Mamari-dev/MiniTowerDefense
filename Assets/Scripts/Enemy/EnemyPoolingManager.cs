using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolingManager : MonoBehaviour
{
    [SerializeField] private EnemyPoolingStruct[] enemyPrefabs;

    public static EnemyPoolingManager Instance { get; private set; }
    private Dictionary<EnemyType, Stack<GameObject>> pools = new();

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
        foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
        {
            pools[type] = new Stack<GameObject>();
        }
    }

    public GameObject GetEnemy(EnemyType type)
    {
        return CheckPoolForObjects(type);
    }

    private GameObject CheckPoolForObjects(EnemyType type)
    {
        if (pools[type].Count == 0)
        {
            for (int i = 0; i < enemyPrefabs.Length; i++)
            {
                if (enemyPrefabs[i].enemyType == type)
                    return Instantiate(enemyPrefabs[i].enemyPrefab, transform);
            }

        }

        return pools[type].Pop();
    }

    public void BackInPool(GameObject poolObject, EnemyType type)
    {
        if (pools[type].Contains(poolObject))
            Debug.LogError($"Doppelter BackInPool! \n" +
                $"{System.Environment.StackTrace}", poolObject);

        pools[type].Push(poolObject);
        poolObject.SetActive(false);
    }
}
