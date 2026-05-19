using System;
using UnityEngine;

public class PlayerInputTowerPlacer
{
    private Vector3Int gridPosition;

    public GameTiles GetClickedTile(Vector2 mousePosition)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
        mouseWorldPos.z = 0;

        gridPosition = TilemapManager.Instance.GetGridPosition(mouseWorldPos);
        GameTiles clickedTile = TilemapManager.Instance.GetTile(gridPosition);

        return clickedTile;
    }

    #region CheckPlacing Conditions
    /// <summary>
    /// return false if the tile has a turret if not return true
    /// </summary>
    /// <returns></returns>
    public bool CheckClickedTile()
    {
        return MapManager.Instance.IsTileBuildableBlocked(gridPosition);
    }

    public bool CheckPlacingConditions(GameObject ghostTower, TowerBaseStats towerStats, Func<bool> isPathAvailable)
    {
        if (HasGhostTower(ghostTower) && CheckTowerCost(towerStats) && CheckPath(isPathAvailable))
            return true;
        return false;
    }

    private bool HasGhostTower(GameObject ghostTower)
    {
        return ghostTower != null;
    }

    private bool CheckTowerCost(TowerBaseStats towerStats)
    {
        if (towerStats != null)
            return CurrencyManager.Instance.CheckCurrencyAmount(towerStats.currencyType, towerStats.cost);
        return false;
    }

    private bool CheckPath(Func<bool> isPathAvailable)
    {
        MapManager.Instance.BlockTile(gridPosition, null, false, false);

        if (isPathAvailable != null && !isPathAvailable())
        {
            MapManager.Instance.BlockTile(gridPosition, null, false, true);
            return false;
        }

        return true;
    }
    #endregion

    public Vector3Int GetGridPositon()
    {
        return gridPosition;
    }

    #region GetClickedTower and Change Visuals
    public GameObject GetClickedTower(GameObject clickedTower)
    {
        GameObject tower = MapManager.Instance.GetPlacedTower(gridPosition);
        if (tower == null) return tower;

        if (clickedTower != null)
        {
            ChangeAttackRangeVisual(clickedTower);
            ChangeTowerCanvas(clickedTower);
        }

        ChangeAttackRangeVisual(tower);
        ChangeTowerCanvas(tower);
        return tower;
    }

    /// <summary>
    /// always return null to delete currentClickTower
    /// </summary>
    /// <param name="clickedTower"></param>
    /// <returns></returns>
    public GameObject DeselectClickedTower(GameObject clickedTower)
    {
        if (clickedTower != null)
        {
            ChangeAttackRangeVisual(clickedTower);
            ChangeTowerCanvas(clickedTower);
        }

        return null;
    }

    private void ChangeAttackRangeVisual(GameObject clickedTower)
    {
        AttackRangeVisual attackRangeVisual;
        attackRangeVisual = clickedTower.GetComponentInChildren<AttackRangeVisual>();
        if (attackRangeVisual != null)
            attackRangeVisual.EnAndDisableRenderer();
    }

    private void ChangeTowerCanvas(GameObject clickedTower)
    {
        TowerCombatCanvas towerCombatCanvas;
        towerCombatCanvas = clickedTower.GetComponentInChildren<TowerCombatCanvas>();
        if (towerCombatCanvas != null)
            towerCombatCanvas.EnAndDisableCanvas();
    }
    #endregion


}
