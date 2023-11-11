using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    [Tooltip("Add Whole Character Panel here (Arts, Stats, and Actions)")]
    [SerializeField] private GameObject characterPanel;
    [Tooltip("Add Action Panel Only")]
    [SerializeField] private GameObject actionPanel;
    [Tooltip("Add items Panel (Body Right)")]
    [SerializeField] private GameObject itemsPanel;
    [Tooltip("Returns Stats from characterPanel")]
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI ATK;
    [SerializeField] private TextMeshProUGUI DEF;
    [SerializeField] private TextMeshProUGUI DEX;

    CommandInput commandInput;
    Factions currentTurn;

    SelectCharacter selectCharacter;
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.UI.CHARACTER_MOVE, this.MoveCommandSelected);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.CHARACTER_ATTK, this.AttkCommandSelected);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.CHARACTER_WAIT, this.WaitCommandSelected);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.CHARACTER_ITEM, this.OpenItemsPanel);
        commandInput = GetComponent<CommandInput>();
        selectCharacter = GetComponent<SelectCharacter>();


    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.CHARACTER_MOVE);
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.CHARACTER_WAIT);
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.CHARACTER_ATTK);
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.CHARACTER_ITEM);
    }
    public void OpenPanel(CharacterTurn characterTurn)
    {
        
        selectCharacter.enabled = false;
        characterPanel.SetActive(true);
        UpdateStatsText();
        currentTurn = RoundManager.instance.GetCurrentTurn();
        
        if (characterTurn.Faction != currentTurn || characterTurn.Act == false)
        {
            actionPanel.SetActive(false);    
        }
        else
        {
            actionPanel.SetActive(true);
        }
    }
    public void ClosePanel()
    {
       characterPanel.SetActive(false);
       itemsPanel.SetActive(false);
    }

    public void MoveCommandSelected()
    {
        if (selectCharacter.selected.GetComponent<CharacterTurn>().Walk)
        {
            commandInput.SetCommandType(CommandType.MoveTo);
            commandInput.InitCommand();
        }
    }
    private void AttkCommandSelected()
    {
        if (selectCharacter.selected.GetComponent<CharacterTurn>().Act)
        {
            commandInput.SetCommandType(CommandType.Attack);
            commandInput.InitCommand();
            ClosePanel();
        }
    }

    private void WaitCommandSelected()
    {
        if (selectCharacter.selected.GetComponent<CharacterTurn>().Walk
            || selectCharacter.selected.GetComponent<CharacterTurn>().Act)
        {
            commandInput.SetCommandType(CommandType.Wait);
            commandInput.InitCommand();
            ClosePanel();
        }
    }
    public void CancelCommandSelected()
    {
        
        ClosePanel();
        selectCharacter.enabled = true;
        selectCharacter.Deselect();
        
    }

    private void OpenItemsPanel()
    {
        itemsPanel.SetActive(true);
    }

    private void UpdateStatsText()
    {
        HP.SetText("HP: " + selectCharacter.selected.statsTemplate.HP.current);
        ATK.SetText("ATK: " + selectCharacter.selected.statsTemplate.damage);
        DEF.SetText("DEF: " + selectCharacter.selected.statsTemplate.DEF);
        DEX.SetText("DEX: " + selectCharacter.selected.statsTemplate.DEX);
    }

}
