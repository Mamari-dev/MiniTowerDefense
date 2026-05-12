using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GameTiles", menuName = "Scriptable Objects/GameTiles")]
public class GameTiles : Tile
{
    [SerializeField] private bool isBuildable;
    [SerializeField] private bool isPath;
    [SerializeField] private bool startTile;
    [SerializeField] private bool endTile;

    public bool IsBuildable { get => isBuildable; set => isBuildable = value; }
    public bool IsPath { get => isPath; set => isPath = value; }
    public bool StartTile { get => startTile; private set => startTile = value; }
    public bool EndTile { get => endTile; private set => endTile = value; }
}
