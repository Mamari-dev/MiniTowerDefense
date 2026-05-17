using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public static TilemapManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public Vector3Int GetGridPosition(Vector3 mouseWorldPos)
    {
        return tilemap.WorldToCell(mouseWorldPos);
    }

    public GameTiles GetTile(Vector3Int gridPosition)
    {
        return tilemap.GetTile<GameTiles>(gridPosition);
    }

    public Vector3 GetWorldPosition(Vector3Int gridPosition)
    {
        return tilemap.GetCellCenterWorld(gridPosition);
    }
}
