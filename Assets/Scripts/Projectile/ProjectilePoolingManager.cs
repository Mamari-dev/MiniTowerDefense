using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolingManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;

    public static ProjectilePoolingManager Instance { get; private set; }
    private Stack<GameObject> poolStack = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public GameObject GetProjectile()
    {
        return CheckPoolForObject();
    }

    private GameObject CheckPoolForObject()
    {
        if (poolStack.Count == 0)
            return Instantiate(projectilePrefab, transform);
        else
            return poolStack.Pop();
    }

    public void BackInPool(GameObject poolObject)
    {
        poolStack.Push(poolObject);
        poolObject.SetActive(false);
    }
}
