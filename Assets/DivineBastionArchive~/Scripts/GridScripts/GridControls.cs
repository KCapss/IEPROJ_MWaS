using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControls : MonoBehaviour
{

    [SerializeField] private Grid targetGrid;
    [SerializeField] private LayerMask terrainLayerMask;
    [Tooltip("DO NOT PUT ANYTHING HERE")]
    [SerializeField] private GridObject hoverOver;
    [Tooltip("DO NOT PUT ANYTHING HERE")]
    [SerializeField] private SelectableGridObject selectedObject;

    private Vector2Int currentGridPos;
    private void Update()
    {
        HoverOverObjectCheck();
        SelectObjectOnClick();
        DeselectObjectOnClick();
    }
    /*
     *  Deselect Character on right mouse click
     */
    private void DeselectObjectOnClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedObject = null;
        }
    }
    /*
     *  Select Character on left mouse click
     */
    private void SelectObjectOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverOver == null) { return; }
            selectedObject = hoverOver.GetComponent<SelectableGridObject>();
        }
    }
    private void HoverOverObjectCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, terrainLayerMask))
        {
            Vector2Int gridPosition = targetGrid.GetGridPosition(hit.point);
            if (gridPosition == currentGridPos) { return; }
            currentGridPos = gridPosition;
            GridObject gridObject = targetGrid.GetPlacedObject(gridPosition);
            hoverOver = gridObject;
        }
    }
}
