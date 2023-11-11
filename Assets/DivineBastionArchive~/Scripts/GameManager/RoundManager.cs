using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    [SerializeField] private TMPro.TextMeshProUGUI turnCountText;
    [SerializeField] private TMPro.TextMeshProUGUI currentTurnText;
    [SerializeField] private ForceContainer playerContainer;
    [SerializeField] private ForceContainer enemyContainer;
    int round = 1;

    Factions currentTurn;
    CommandInput commandInput;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        EventBroadcaster.Instance.AddObserver(EventNames.UI.END_TURN, this.NextTurn);
        commandInput = GetComponent<CommandInput>();
    }
    private void Start()
    {
        UpdateTextOnScreen();
    }
    public void AddMe(CharacterTurn character)
    {
        if(character.Faction == Factions.Player)
        {
            playerContainer.AddMe(character);
        }
        if (character.Faction == Factions.Opponent)
        {
            enemyContainer.AddMe(character);
        }
    }
    private void NextTurn()
    {
        switch (currentTurn)
        {
            case Factions.Player:
                currentTurn = Factions.Opponent;
                break;
            case Factions.Opponent:
                NextRound();
                currentTurn = Factions.Player;
                break;
        }

        commandInput.StopCurrentCommandInput();
        GrantTurnToForce();
        UpdateTextOnScreen();
    }

    private void GrantTurnToForce()
    {

        switch (currentTurn)
        {
            case Factions.Player:
                playerContainer.GrantTurn();
                break;
            case Factions.Opponent:
                enemyContainer.GrantTurn();
                break;
        }
    }

    public void NextRound()
    {
        round++;
    }

    public void UpdateTextOnScreen()
    {
        turnCountText.text = "Turn: " + round.ToString();
        currentTurnText.text = currentTurn.ToString();
    }

    public Factions GetCurrentTurn()
    {
        return currentTurn;
    }
}
