using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GameTiles", menuName = "Scriptable Objects/GameTiles")]
public class GameTiles : Tile
{
    [SerializeField] private GameTileStruct tileStruct;
    [SerializeField] private bool startTile;
    [SerializeField] private bool endTile;

    public GameTileStruct TileStruct { get => tileStruct; set => tileStruct = value; }
    public bool StartTile { get => startTile; private set => startTile = value; }
    public bool EndTile { get => endTile; private set => endTile = value; }
}
