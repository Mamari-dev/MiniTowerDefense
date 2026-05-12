using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseClick : MonoBehaviour
{
    [SerializeField] private Tilemap buildTileMap;
    [SerializeField] private GameObject towerPrefab;
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
            Vector3 spawnPos = buildTileMap.GetCellCenterWorld(gridPos);
            GameObject newTower = Instantiate(towerPrefab);
            newTower.transform.position = spawnPos;

            GameTileStruct data = new GameTileStruct
            {
                isBuildable = false,
                isPath = false,
            };
            MapManager.Instance.BlockTile(gridPos, data);
        }
        else
            Debug.Log($"fail: {clickedTile}");
    }
}
