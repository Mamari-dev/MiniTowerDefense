using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    private GameObject ghostTower;
    private TowerBaseStats towerStats;
    private GameObject clickedTower;
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

    /// <summary>
    /// if tile is not buildable or null, stop leftclick here
    /// </summary>
    private void LeftClick()
    {
        //DeselectClickedTower();

        GameTiles clickedTile = GetClickedTile();
        if (clickedTile == null || !clickedTile.TileStruct.isBuildable) return;

        if (CheckClickedTile() && HasGhostTower() && CheckTowerCost() && CheckPath())
            PlaceTower();
        else if (!CheckClickedTile())
        {
            StopGhostFollowCoroutine();
            CancelPlacement();

            GetClickedTower();
        }
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

        gridPosition = TilemapManager.Instance.GetGridPosition(mouseWorldPos);
        GameTiles clickedTile = TilemapManager.Instance.GetTile(gridPosition);

        return clickedTile;
    }

    /// <summary>
    /// return false if the tile has a turret if not return true
    /// </summary>
    /// <returns></returns>
    private bool CheckClickedTile()
    {
        return MapManager.Instance.IsTileBuildableBlocked(gridPosition);
    }

    private bool CheckPath()
    {
        MapManager.Instance.BlockTile(gridPosition, null, false, false);

        if (IsPathAvailable != null && !IsPathAvailable())
        {
            MapManager.Instance.BlockTile(gridPosition, null, false, true);
            return false;
        }

        return true;
    }

    private void PlaceTower()
    {
        CurrencyManager.Instance.BuyTower(towerStats.currencyType, towerStats.cost);

        Vector3 spawnPos = TilemapManager.Instance.GetWorldPosition(gridPosition);
        GameObject newTower = Instantiate(towerStats.prefab);
        newTower.transform.position = spawnPos;

        MapManager.Instance.BlockTile(gridPosition, newTower, false, false);
    }

    private void GetClickedTower()
    {
        GameObject tower = MapManager.Instance.GetPlacedTower(gridPosition);
        if (tower == null) return;

        if (clickedTower != null)
        {
            ChangeAttackRangeVisual();
            ChangeTowerCanvas();
        }

        clickedTower = tower;
        ChangeAttackRangeVisual();
        ChangeTowerCanvas();
    }

    private void ChangeAttackRangeVisual()
    {
        AttackRangeVisual attackRangeVisual;
        attackRangeVisual = clickedTower.GetComponentInChildren<AttackRangeVisual>();
        if (attackRangeVisual != null)
            attackRangeVisual.EnAndDisableRenderer();
    }

    private void ChangeTowerCanvas()
    {
        TowerCombatCanvas towerCombatCanvas;
        towerCombatCanvas = clickedTower.GetComponentInChildren<TowerCombatCanvas>();
        if (towerCombatCanvas != null)
            towerCombatCanvas.EnAndDisableCanvas();
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
        DeselectClickedTower();
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

    private void DeselectClickedTower()
    {
        if (clickedTower != null)
        {
            ChangeAttackRangeVisual();
            ChangeTowerCanvas();

            clickedTower = null;
        }
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

        AttackRangeVisual attackRangeVisualDrawer = ghostTower.GetComponentInChildren<AttackRangeVisual>();
        if (attackRangeVisualDrawer != null)
        {
            TowerCombatStats combatStats = towerStats.prefab.GetComponent<TowerCombat>().TowerCombatStats;
            if (combatStats != null)
                attackRangeVisualDrawer.DrawAttackRange(combatStats.attackRange);
        }

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

            Vector3Int gridPos = TilemapManager.Instance.GetGridPosition(mouseWorldPos);
            Vector3 snapPos = TilemapManager.Instance.GetWorldPosition(gridPos);

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

    /// <summary>
    /// 1 stand for breakscene in build options == index 1
    /// </summary>
    /// <param name="context"></param>
    public void OnEscapeClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (SpeedController.Instance.isActiveAndEnabled)
            {
                SpeedController.Instance.ChangeEnabled();
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
            }
            else
            {
                SpeedController.Instance.ChangeEnabled();
                SceneManager.UnloadSceneAsync(1);
            }
        }
    }
}
