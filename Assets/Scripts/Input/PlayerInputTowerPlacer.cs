using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

    public bool HasGhostTower(GameObject ghostTower, TowerBaseStats ghostTowerStats)
    {
        return ghostTower != null && ghostTowerStats != null;
    }

    public bool CheckPlacingConditions(TowerBaseStats ghostTowerStats, Func<bool> isPathAvailable)
    {
        if (CheckClickedTile(ghostTowerStats) && CheckTowerCost(ghostTowerStats) && CheckPath(isPathAvailable))
            return true;
        return false;
    }
    private bool CheckClickedTile(TowerBaseStats towerStats)
    {
        if (MapManager.Instance.IsTileBuildableBlocked(gridPosition) &&
            MapManager.Instance.GetPlacedTowerType(gridPosition) != towerStats.towerType)
            return true;

        return false;
    }

    private bool CheckTowerCost(TowerBaseStats towerStats)
    {
        if (towerStats != null)
            return CurrencyManager.Instance.CheckCurrencyAmount(towerStats.currencyType, towerStats.cost);
        return false;
    }

    private bool CheckPath(Func<bool> isPathAvailable)
    {
        MapManager.Instance.BlockTile(gridPosition, null, TowerTypes.None, false, false);

        if (isPathAvailable != null && !isPathAvailable())
        {
            MapManager.Instance.BlockTile(gridPosition, null, TowerTypes.None, false, true);
            return false;
        }

        return true;
    }
    #endregion

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
        TowerNonCombatCanvas towerCombatCanvas;
        towerCombatCanvas = clickedTower.GetComponentInChildren<TowerNonCombatCanvas>();
        if (towerCombatCanvas != null)
            towerCombatCanvas.EnAndDisableCanvas();
    }
    #endregion

    public Vector3 BuyTower(TowerBaseStats towerStats)
    {
        CurrencyManager.Instance.BuyTower(towerStats.currencyType, towerStats.cost);
        return TilemapManager.Instance.GetWorldPosition(gridPosition);
    }

    public void SetTileValues(GameObject tower, TowerBaseStats towerStats)
    {
        if (towerStats.towerType == TowerTypes.BlockTower)
            MapManager.Instance.BlockTile(gridPosition, tower, towerStats.towerType, true, false);
        else
            MapManager.Instance.BlockTile(gridPosition, tower, towerStats.towerType, false, false);
    }
}
