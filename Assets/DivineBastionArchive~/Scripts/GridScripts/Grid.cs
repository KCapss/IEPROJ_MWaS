using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
/*
 * Grid System - 2D Array with nodes in each element
 */
public class Grid : MonoBehaviour
{
    [Header("Grid Properties")]
    [Tooltip("Level Size")]
    [SerializeField] public int _width = 10;
    [Tooltip("Level Size")]
    [SerializeField] public int _height = 10;
    [Tooltip("Size of each tile")]
    [SerializeField] private float _cellSize;
    [Tooltip("Layer mask for walls or obstacles. Objects set here will be unpassable")]
    [SerializeField] private LayerMask _obstacleLayer;
    [Tooltip("Layer mask for floor")]
    [SerializeField] private LayerMask _terrainLayer;
    private Node[,] grid;
    private void Awake()
    {
        GenerateGrid();
    }
    private void GenerateGrid()
    {
        grid = new Node[_width, _height];
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                grid[x, y] = new Node();
            }
        }
        CalculateElevation();
        CheckPassableTerrain();
    }
    /*
     * checks each tile if there are any obstacles in its area
     * obstacles are counted even if they don't cover the whole tile
     */
    private void CheckPassableTerrain()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 worldPosition = GetWorldPosition(x, y);
                bool passable = !Physics.CheckBox(worldPosition, Vector3.one / 2 * _cellSize * 0.9f, Quaternion.identity, _obstacleLayer);
                grid[x, y].passable = passable;
            }
        }
    }
    /*
     * Sends rays downward, first collision with terrain is the elevation of the tile
     */
    private void CalculateElevation()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Ray ray = new Ray(GetWorldPosition(x, y) + Vector3.up * 100f, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.MaxValue, _terrainLayer))
                {
                    grid[x, y].elevation = hit.point.y;
                }
            }
        }

    }
    private void OnDrawGizmos()
    {
        if (grid == null)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Vector3 pos = GetWorldPosition(x, y);
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }
        else 
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    Vector3 pos = GetWorldPosition(x, y, true);
                    Gizmos.color = grid[x, y].passable ? Color.white : Color.red;
                    Gizmos.DrawCube(pos, Vector3.one / 4);
                }
            }
        }

        
    }
    public Vector3 GetWorldPosition(int x, int y, bool elevation = false)
    {
        return new Vector3(x * _cellSize, elevation == true ? grid[x, y].elevation : 0f, y * _cellSize);
    }
    public void PlaceObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = gridObject;
            grid[positionOnGrid.x, positionOnGrid.y].passable = false;
        }
        else
        {
            Debug.Log("OUT OF BOUNDS ACCESS");
        }
    }
    public void RemoveObject(Vector2Int positionOnGrid, GridObject gridObject)
    {
        if (CheckBoundry(positionOnGrid))
        {
            grid[positionOnGrid.x, positionOnGrid.y].gridObject = null;
            grid[positionOnGrid.x, positionOnGrid.y].passable = true;
        }
        else 
        {
            Debug.Log("OUT OF BOUNDS ACCESS");
        }
    }
    public bool CheckBoundry(Vector2Int positionOnGrid)
    {
        if (positionOnGrid.x < 0 || positionOnGrid.x >= _width)
        { 
            return false; 
        }
        if (positionOnGrid.y < 0 || positionOnGrid.y >= _height)
        {
            return false;
        }

        return true;
    }
    public bool CheckBoundry(int xPos, int yPos)
    {
        if (xPos < 0 || xPos >= _width)
        {
            return false;
        }
        if (yPos < 0 || yPos >= _height)
        {
            return false;
        }

        return true;
    }

    public bool CheckWalkable(int pos_x, int pos_y)
    {
        return grid[pos_x, pos_y].passable;
    }
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        worldPosition.x += _cellSize / 2;
        worldPosition.z += _cellSize / 2;
        Vector2Int positionOnGrid = new Vector2Int((int)(worldPosition.x / _cellSize), (int)(worldPosition.z / _cellSize));
        return positionOnGrid;
    }
    public GridObject GetPlacedObject(Vector2Int gridPosition)
    {
        if (CheckBoundry(gridPosition))
        {
            GridObject gridObject = grid[gridPosition.x, gridPosition.y].gridObject;
            return gridObject;
        }

        return null;
    }
    public List<Vector3> ConvertPathNodeToTargetPositions(List<PathNode> path)
    {
        List<Vector3> worldPositions = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            worldPositions.Add(GetWorldPosition(path[i].pos_x, path[i].pos_y, true));
        }

        return worldPositions;
        
    }
   
}