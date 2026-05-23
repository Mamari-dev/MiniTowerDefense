using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolingManager : MonoBehaviour
{
    [SerializeField] private ProjectilePoolingStruct[] projectilePrefabs;

    public static ProjectilePoolingManager Instance { get; private set; }
    private Dictionary<ProjectileTypes, Stack<GameObject>> pools = new();

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
        foreach (ProjectileTypes type in System.Enum.GetValues(typeof(EnemyType)))
        {
            pools[type] = new Stack<GameObject>();
        }
    }

    public GameObject GetProjectile(ProjectileTypes type)
    {
        return CheckPoolForObject(type);
    }

    private GameObject CheckPoolForObject(ProjectileTypes type)
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

    public void BackInPool(GameObject poolObject, ProjectileTypes type)
    {
        pools[type].Push(poolObject);
        poolObject.SetActive(false);
    }
}
