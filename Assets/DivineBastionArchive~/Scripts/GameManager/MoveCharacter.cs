using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [SerializeField] private Grid targetGrid;
    [SerializeField] private GridHighlight gridMoveHighlight;
    Pathfinding pathfinding;

    
    private void Start()
    {
        pathfinding = targetGrid.GetComponent<Pathfinding>();
    }

    public void CheckWalkableTerrain(Character targetCharacter)
    {
        GridObject gridObject = targetCharacter.GetComponent<GridObject>();
        List<PathNode> walkableNodes = new List<PathNode>();
        pathfinding.Clear();
        pathfinding.CalculateWalkableNodes(
            gridObject.positionOnGrid.x,
            gridObject.positionOnGrid.y,
            targetCharacter.MovementPoints,
            ref walkableNodes);
        gridMoveHighlight.Hide();
        gridMoveHighlight.Highlight(walkableNodes);
    }
    public List<PathNode> GetPath(Vector2Int from)
    {
        List<PathNode> path = pathfinding.TraceBackPath(from.x, from.y);

        if (path == null) { return null; }
        if (path.Count == 0) { return null; }

        path.Reverse();
        return path;
    }
}
