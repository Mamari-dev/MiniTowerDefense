using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTowerManager : MonoBehaviour
{
    [SerializeField] private Tilemap buildTileMap;
    private GameObject ghostTower;
    private TowerStats towerStats;

    public static PlaceTowerManager instance;
    public static Func<bool> OnClick;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (ghostTower == null) return;

        GhostFollowMouse();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            CheckTile();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            CancelPlacement();
    }

    private void CheckTile()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        Vector3Int gridPos = buildTileMap.WorldToCell(mouseWorldPos);
        GameTiles clickedTile = buildTileMap.GetTile<GameTiles>(gridPos);

        if (clickedTile != null && clickedTile.TileStruct.isBuildable && MapManager.Instance.IsTileBuildableBlocked(gridPos))
        {
            //fake tower, for checking path
            GameTileStruct fakeData = new()
            {
                isBuildable = false,
                isPath = false,
            };
            MapManager.Instance.BlockTile(gridPos, fakeData);

            if (OnClick != null && !OnClick())
            {
                GameTileStruct data = new()
                {
                    isBuildable = false,
                    isPath = true,
                };
                MapManager.Instance.BlockTile(gridPos, data);
            }
            else
            {
                Vector3 spawnPos = buildTileMap.GetCellCenterWorld(gridPos);
                GameObject newTower = Instantiate(towerStats.prefab);
                newTower.transform.position = spawnPos;
            }
        }
    }

    private void CancelPlacement()
    {
        Destroy(ghostTower);
        ghostTower = null;
        towerStats = null;
    }

    private void GhostFollowMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        Vector3Int gridPos = buildTileMap.WorldToCell(mouseWorldPos);
        Vector3 snapPos = buildTileMap.GetCellCenterWorld(gridPos);

        ghostTower.transform.position = snapPos;
    }

    public void GetTower(TowerStats stats)
    {
        if (ghostTower != null) Destroy(ghostTower);

        towerStats = stats;
        ghostTower = Instantiate(stats.ghostPrefab);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        ghostTower.transform.position = mouseWorldPos;
    }

    private void OnDisable()
    {
        OnClick = null;
    }
}
