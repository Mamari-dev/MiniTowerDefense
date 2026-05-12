using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    private Dictionary<Vector3Int, bool> blockedTiles = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void BlockTile(Vector3Int pos)
    {
        blockedTiles[pos] = true;
    }

    public bool IsTileBlocked(Vector3Int pos)
    {
        return blockedTiles.ContainsKey(pos) && blockedTiles[pos];
    }
}
