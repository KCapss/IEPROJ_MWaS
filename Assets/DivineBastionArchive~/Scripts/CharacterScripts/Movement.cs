using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    GridObject gridObject;
    List<Vector3> pathWorldPositions;
    [SerializeField] private float movespeed = 1f;

    private void Awake()
    {
        gridObject = GetComponent<GridObject>();
    }

    public void Move(List<PathNode> path)
    {
        pathWorldPositions = gridObject.targetGrid.ConvertPathNodeToTargetPositions(path);
        gridObject.targetGrid.RemoveObject(gridObject.positionOnGrid, gridObject); // remove object from grid data
        gridObject.positionOnGrid.x = path[path.Count-1].pos_x;
        gridObject.positionOnGrid.y = path[path.Count-1].pos_y;
        gridObject.targetGrid.PlaceObject(gridObject.positionOnGrid, gridObject); // update grid data
    }

    private void Update()
    {
        if (pathWorldPositions == null) { return; }
        if (pathWorldPositions.Count == 0) { return; }

        transform.position = Vector3.MoveTowards(transform.position, pathWorldPositions[0], movespeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pathWorldPositions[0]) < 0.05f)
        {
            pathWorldPositions.RemoveAt(0);
        }
            
    }
}
