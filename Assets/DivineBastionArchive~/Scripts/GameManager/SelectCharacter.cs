using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    MouseInput mouseInput;
    CommandMenu menu;
    public Character selected;
    public Character hoverOverCharacter;
    private GridObject hoverOverGridObject;
    [SerializeField] private Grid targetGrid;
    private Vector2Int positionOnGrid = new Vector2Int(-1, -1);
    Factions currentTurn;

    private void Awake()
    {
        mouseInput = GetComponent<MouseInput>();
        menu = GetComponent<CommandMenu>();
    }
    private void Update()
    {
        HoverOverObject();
        SelectGridObject();
        //DeselectGridObject();
    }

    private void HoverOverObject()
    {
        if (positionOnGrid != mouseInput.positionOnGrid)
        {
            positionOnGrid = mouseInput.positionOnGrid;
            hoverOverGridObject = targetGrid.GetPlacedObject(positionOnGrid);
            if (hoverOverGridObject != null)
            {
                hoverOverCharacter = hoverOverGridObject.GetComponent<Character>();
            }
            else
            {
                hoverOverCharacter = null;
            }
        }
    }
    public void DeselectGridObject()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
            UpdatePanel();
        }
    }
    
    public void SelectGridObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentTurn = RoundManager.instance.GetCurrentTurn();
            if (hoverOverCharacter != null && selected != hoverOverCharacter)
            {
                selected = hoverOverCharacter;
                if (selected.Faction == currentTurn)
                {
                    EventBroadcaster.Instance.PostEvent(EventNames.UI.CHARACTER_MOVE);
                }
            }
            UpdatePanel();
        }
        /*
        if (Input.GetMouseButton(1))
        {
            if (selected != null)
            {
                DeselectGridObject();
            }
        }
        */
        
    }
 
    private void UpdatePanel()
    {
        if (selected != null)
        {
            menu.OpenPanel(selected.GetComponent<CharacterTurn>());
        }
        else
        {
            menu.ClosePanel();
        }
    }
    public void Deselect()
    {
        selected = null;
        UpdatePanel();
    }
}
