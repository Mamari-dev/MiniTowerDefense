using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private LineRenderer lineRenderer;
    private Vector3Int startTilePos;
    private Vector3Int endTilePos;
    private List<Vector3Int> currentPath;

    private void Start()
    {
        FindStartAndEndTilePos();
        FindPath();
    }

    private void FindStartAndEndTilePos()
    {
        BoundsInt bounds = tileMap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            GameTiles tile = tileMap.GetTile<GameTiles>(pos);

            if (tile == null)
                continue;
            if (tile.StartTile)
                startTilePos = pos;
            if (tile.EndTile)
                endTilePos = pos;
        }
    }

    private void FindPath()
    {
        List<PathNode> openList = new();
        HashSet<Vector3Int> closedList = new();

        PathNode startNode = CreateNewPathNode(startTilePos, 0, null);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < currentNode.fCost ||
                    openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode.position);

            if (currentNode.position == endTilePos)
            {
                currentPath = CalculatePath(startNode, currentNode);
                DrawPath();
                break;
            }

            foreach (Vector3Int neightborPos in GetWalkableNeighbors(currentNode.position))
            {
                if (closedList.Contains(neightborPos))
                    continue;

                int gCostNew = currentNode.gCost + GetDistance(currentNode.position, neightborPos);
                PathNode neightborNode = openList.Find(n => n.position == neightborPos);

                if (neightborNode == null)
                {
                    openList.Add(CreateNewPathNode(neightborPos, gCostNew, currentNode));
                }
                else if (gCostNew < neightborNode.gCost)
                {
                    neightborNode.gCost = gCostNew;
                    neightborNode.parent = currentNode;
                }
            }
        }
    }

    private PathNode CreateNewPathNode(Vector3Int pos, int gCost, PathNode parent)
    {
        PathNode pathNode = new PathNode(pos);
        pathNode.gCost = gCost;
        pathNode.hCost = GetDistance(pos, endTilePos);
        pathNode.parent = parent;

        return pathNode;
    }

    private int GetDistance(Vector3Int currentPos, Vector3Int endPos)
    {
        Vector3 currentWorldPos = tileMap.CellToWorld(currentPos);
        Vector3 endWorldPos = tileMap.CellToWorld(endPos);
        return Mathf.RoundToInt(Vector3.Distance(currentWorldPos, endWorldPos) * 10);
    }

    private List<Vector3Int> GetWalkableNeighbors(Vector3Int currentPos)
    {
        List<Vector3Int> neighbors = new();
        Vector3Int[] directions;

        if (Mathf.Abs(currentPos.y) % 2 == 1) //ungerade Spalte
        {
            directions = new Vector3Int[]
            {
                new Vector3Int(1,0,0), //oben
                new Vector3Int(1,1,0), //oben rechts
                new Vector3Int(0,1,0), //unten-rechts
                new Vector3Int(-1,0,0), //unten
                new Vector3Int(0,-1,0), //unten-links
                new Vector3Int(1,-1,0), //oben-links
            };
        }
        else //gerade Spalte
        {
            directions = new Vector3Int[]
            {
                new Vector3Int(1,0,0), //oben
                new Vector3Int(0,1,0), //oben-rechts
                new Vector3Int(-1,1,0), //unten-rechts
                new Vector3Int(0,-1,0), //unten
                new Vector3Int(-1,0,0), //unten-links
                new Vector3Int(-1,-1,0), //oben-links
            };
        }

        foreach (Vector3Int offset in directions)
        {
            Vector3Int neighborPos = currentPos + offset;
            GameTiles tile = tileMap.GetTile<GameTiles>(neighborPos);
            if (tile != null && tile.TileStruct.isPath && MapManager.Instance.IsTilePathBlocked(neighborPos))
                neighbors.Add(neighborPos);
        }

        return neighbors;
    }

    private List<Vector3Int> CalculatePath(PathNode startNode, PathNode endNode)
    {
        List<Vector3Int> path = new();
        PathNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }

        path.Add(startNode.position);
        path.Reverse();
        return path;
    }

    private void DrawPath()
    {
        if (currentPath == null || currentPath.Count == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = currentPath.Count;

        for (int i = 0; i < currentPath.Count; i++)
        {
            Vector3 worldPos = tileMap.GetCellCenterWorld(currentPath[i]);
            worldPos.z = -1;

            lineRenderer.SetPosition(i, worldPos);
        }
    }

    public List<Vector3Int> GetPath()
    {
        return currentPath;
    }

    [ContextMenu("Test Pathfinding")]
    public void TestPath()
    {
        FindStartAndEndTilePos(); // Erst die Punkte finden
        FindPath();            // Dann berechnen
                               // Durch OnDrawGizmos wird er jetzt sofort in der Scene-Ansicht erscheinen!
    }
}
