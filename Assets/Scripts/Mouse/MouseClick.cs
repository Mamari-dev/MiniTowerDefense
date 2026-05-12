using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private Tilemap buildTileMap;
    [SerializeField] private GameObject towerPrefab;

    public static Func<bool> OnClick;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CheckTile();
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
                GameObject newTower = Instantiate(towerPrefab);
                newTower.transform.position = spawnPos;
            }
        }
    }

    private void OnDisable()
    {
        OnClick = null;
    }
}
