using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private GameObject ghostTower;
    private TowerBaseStats towerStats;
    private Vector3Int gridPosition;
    private Vector2 mousePosition;
    private Coroutine ghostFollowMouseCoroutine;

    public static Func<bool> IsPathAvailable; //check in "Pathfinding", is path available if tower placed?

    public static PlayerInput Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        WaveManager.StartWave += StopGhostFollowCoroutine;
        WaveManager.StartWave += CancelPlacement;
    }

    #region left click
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
            LeftClick();
    }

    private void LeftClick()
    {
        if (!HasGhostTower() || !CheckTowerCost()) return;

        GameTiles clickedTile = GetClickedTile();
        if (clickedTile == null) return;

        if (!CheckPath(clickedTile)) return;

        PlaceTower();
    }

    private bool HasGhostTower()
    {
        return (ghostTower == null) ? false : true;
    }

    private bool CheckTowerCost()
    {
        return CurrencyManager.Instance.CheckCurrencyAmount(towerStats.currencyType, towerStats.cost);
    }

    private GameTiles GetClickedTile()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.z = 0;

        gridPosition = tilemap.WorldToCell(mouseWorldPos);
        GameTiles clickedTile = tilemap.GetTile<GameTiles>(gridPosition);

        return clickedTile;
    }

    private bool CheckPath(GameTiles clickedTile)
    {
        if (clickedTile.TileStruct.isBuildable && MapManager.Instance.IsTileBuildableBlocked(gridPosition))
        {
            GameTileStruct fakeData = new()
            {
                isBuildable = false,
                isPath = false,
            };
            MapManager.Instance.BlockTile(gridPosition, fakeData);

            if (IsPathAvailable != null && !IsPathAvailable())
            {
                GameTileStruct data = new()
                {
                    isBuildable = false,
                    isPath = true,
                };
                MapManager.Instance.BlockTile(gridPosition, data);

                return false;
            }
            else
                return true;
        }

        return false;
    }

    private void PlaceTower()
    {
        CurrencyManager.Instance.BuyTower(towerStats.currencyType, towerStats.cost);

        Vector3 spawnPos = tilemap.GetCellCenterWorld(gridPosition);
        GameObject newTower = Instantiate(towerStats.prefab);
        newTower.transform.position = spawnPos;
    }

    #endregion

    #region right click

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.started)
            RightClick();
    }

    private void RightClick()
    {
        StopGhostFollowCoroutine();
        CancelPlacement();
    }

    private void StopGhostFollowCoroutine()
    {
        if (ghostFollowMouseCoroutine != null)
            StopCoroutine(ghostFollowMouseCoroutine);
    }

    private void CancelPlacement()
    {
        Destroy(ghostTower);
        ghostTower = null;
        towerStats = null;
    }
    #endregion

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void GetTower(TowerBaseStats stats)
    {
        if (ghostFollowMouseCoroutine != null)
            StopCoroutine(ghostFollowMouseCoroutine);
        if (ghostTower != null)
            Destroy(ghostTower);

        towerStats = stats;
        ghostTower = Instantiate(stats.ghostPrefab);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        ghostTower.transform.position = mouseWorldPos;

        ghostFollowMouseCoroutine = StartCoroutine(GhostFollowMouse());
    }

    private IEnumerator GhostFollowMouse()
    {
        while (true)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
            mouseWorldPos.z = 0;

            Vector3Int gridPos = tilemap.WorldToCell(mouseWorldPos);
            Vector3 snapPos = tilemap.GetCellCenterWorld(gridPos);

            ghostTower.transform.position = snapPos;

            yield return null;
        }
    }

    private void OnDisable()
    {
        IsPathAvailable = null;

        if (ghostFollowMouseCoroutine != null)
            StopCoroutine(ghostFollowMouseCoroutine);
    }
}
