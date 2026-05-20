using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    private Vector3 startTilePos;
    private List<Vector3> currentWorldPath = new();
    private Dictionary<Vector3Int, GameTileStruct> blockedTiles = new();

    public Tilemap TileMap { get => tileMap; }
    public Vector3 StartTilePos { get => startTilePos; set => startTilePos = value; }
    public List<Vector3> CurrentWorldPath { get => currentWorldPath; set => currentWorldPath = value; }

    public static MapManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void BlockTile(Vector3Int pos, GameObject tower, TowerTypes towerType, bool isBuildable, bool isPath)
    {
        blockedTiles.TryGetValue(pos, out GameTileStruct oldData);
        if (oldData.tower != null)
            Destroy(oldData.tower);

        GameTileStruct newData = new()
        {
            tower = tower,
            towerType = towerType,
            isBuildable = isBuildable,
            isPath = isPath,
        };

        blockedTiles[pos] = newData;
    }

    public bool IsTileBuildableBlocked(Vector3Int pos)
    {
        if (blockedTiles.Count == 0)
            return true;

        if (blockedTiles.ContainsKey(pos) && blockedTiles.TryGetValue(pos, out GameTileStruct datas))
            return datas.isBuildable;

        return true;
    }

    public bool IsTilePathBlocked(Vector3Int pos)
    {
        if (blockedTiles.Count == 0)
            return true;

        if (blockedTiles.ContainsKey(pos) && blockedTiles.TryGetValue(pos, out GameTileStruct datas))
            return datas.isPath;

        return true;
    }

    public GameObject GetPlacedTower(Vector3Int pos)
    {
        blockedTiles.TryGetValue(pos, out GameTileStruct datas);
        return datas.tower;
    }

    public TowerTypes GetPlacedTowerType(Vector3Int pos)
    {
        blockedTiles.TryGetValue(pos, out GameTileStruct datas);
        return datas.towerType;
    }

    public void DestroyTower(Vector3Int pos)
    {
        blockedTiles.TryGetValue(pos, out GameTileStruct datas);
        Destroy(datas.tower);
    }
}
