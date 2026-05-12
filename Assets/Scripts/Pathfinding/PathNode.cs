using UnityEngine;

public class PathNode
{
    public Vector3Int position;
    public PathNode parent;

    public int gCost; //-> start bis actueller tile //bisher gelaufene kosten
    public int hCost; //-> actueller Tile bis Ziel  //luftlinie, geschätze kosten
    public int fCost => gCost + hCost;

    public PathNode(Vector3Int pos)
    {
        position = pos;
    }
}
