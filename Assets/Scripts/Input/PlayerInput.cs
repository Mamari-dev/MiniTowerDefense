using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    private GameObject ghostTower;
    private TowerBaseStats ghostTowerStats;
    private GameObject clickedTower;
    private Vector2 mousePosition;
    private Coroutine ghostFollowMouseCoroutine;
    private PlayerInputTowerPlacer towerPlacer;

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

        towerPlacer = new();
    }

    #region left click
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
            StartCoroutine(LeftClickFrameWaiter());
    }

    /// <summary>
    /// wait for one frame so the ui is not behind the click
    /// when hit a ui element -> do nothing
    /// otherwise try to place tower if ghosttower exist
    /// </summary>
    /// <returns></returns>
    private IEnumerator LeftClickFrameWaiter()
    {
        yield return new WaitForEndOfFrame();

        if (!EventSystem.current.IsPointerOverGameObject())
            LeftClickAfterFrame();
    }

    private void LeftClickAfterFrame()
    {
        clickedTower = towerPlacer.DeselectClickedTower(clickedTower);

        GameTiles clickedTile = towerPlacer.GetClickedTile(mousePosition);
        if (clickedTile == null || !clickedTile.TileStruct.isBuildable) return;

        if (towerPlacer.HasGhostTower(ghostTower, ghostTowerStats) && towerPlacer.CheckPlacingConditions(ghostTowerStats, IsPathAvailable))
            PlaceTower();
        else if (!towerPlacer.HasGhostTower(ghostTower, ghostTowerStats))
        {
            StopGhostFollowCoroutine();
            CancelPlacement();
            clickedTower = towerPlacer.GetClickedTower(clickedTower);
        }
    }

    private void PlaceTower()
    {
        Vector3 spawnPos = towerPlacer.BuyTower(ghostTowerStats);
        GameObject newTower = Instantiate(ghostTowerStats.prefab);
        newTower.transform.position = spawnPos;

        towerPlacer.SetTileValues(newTower, ghostTowerStats);
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
        clickedTower = towerPlacer.DeselectClickedTower(clickedTower);
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
        ghostTowerStats = null;
    }
    #endregion

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
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


    public void GetTower(TowerBaseStats stats)
    {
        if (ghostFollowMouseCoroutine != null)
            StopCoroutine(ghostFollowMouseCoroutine);
        if (ghostTower != null)
            Destroy(ghostTower);

        ghostTowerStats = stats;
        ghostTower = Instantiate(stats.ghostPrefab);

        AttackRangeVisual attackRangeVisualDrawer = ghostTower.GetComponentInChildren<AttackRangeVisual>();
        if (attackRangeVisualDrawer != null)
        {
            if (ghostTowerStats.prefab.TryGetComponent(out TowerCombat towerCombat))
                attackRangeVisualDrawer.DrawAttackRange(towerCombat.GetAttackRange());
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
}
