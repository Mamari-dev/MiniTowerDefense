using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    private Dictionary<Vector3Int, GameTileStruct> blockedTiles = new();

    private Vector3 startTilePos;
    public Vector3 StartTilePos { get => startTilePos; set => startTilePos = value; }

    private List<Vector3> currentWorldPath = new();
    public List<Vector3> CurrentWorldPath { get => currentWorldPath; set => currentWorldPath = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void BlockTile(Vector3Int pos, GameTileStruct tileDatas)
    {
        blockedTiles[pos] = tileDatas;
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
}
