using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlight : MonoBehaviour
{
    Grid grid;
    [Tooltip("Highlight game object")]
    [SerializeField] private GameObject highlightPoint;
    [SerializeField] private GameObject container;
    List<GameObject> highlightPointsGO;
    
    void Awake()
    {
        grid = GetComponentInParent<Grid>();
        highlightPointsGO = new List<GameObject>();
        
    }
    private GameObject CreatePointHighLightObject()
    {
        GameObject go = Instantiate(highlightPoint);
        highlightPointsGO.Add(go);
        go.transform.SetParent(container.transform);
        return go;
    }
    private GameObject GetHighlightPointGO(int i)
    {
        if (highlightPointsGO.Count > i)
        {
            return highlightPointsGO[i];
        }

        GameObject newHighlightObject = CreatePointHighLightObject();
        return newHighlightObject;
    }
    public void Highlight(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].x, positions[i].y, GetHighlightPointGO(i));
        }
    }
    public void Highlight(List<PathNode> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Highlight(positions[i].pos_x, positions[i].pos_y, GetHighlightPointGO(i));
        }
    }
    public void Highlight(int posX, int posY, GameObject highlightObject)
    {
        highlightObject.SetActive(true);
        Vector3 position = grid.GetWorldPosition(posX, posY, true);
        position += Vector3.up * 0.2f;
        highlightObject.transform.position = position;
    }
    public void Hide()
    {
        for (int i = 0; i < highlightPointsGO.Count; i++)
        {
            highlightPointsGO[i].SetActive(false);
        }
    }
}
