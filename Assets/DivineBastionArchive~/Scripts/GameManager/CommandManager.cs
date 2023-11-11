using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum CommandType 
{
    MoveTo = 0,
    Attack,
    Wait,
    Ability,
    Items
}
public class Command
{
    //OnAwake
    public Character character;
    public Vector2Int selectedGrid;
    public CommandType commandType;
    //Variable
    public List<PathNode> path;
    public GridObject target;

    public Command(Character character, Vector2Int selectedGrid, CommandType commandType)
    {
        this.character = character;
        this.selectedGrid = selectedGrid;
        this.commandType = commandType;
    }
}
public class CommandManager : MonoBehaviour
{
    CleanUtility cleanUtility;
    VictoryConditionManager victoryConditionManager;
    Command currentCommand;

    private void Awake()
    {
        cleanUtility = GetComponent<CleanUtility>();
        victoryConditionManager = GetComponent<VictoryConditionManager>();
    }

    private void Update()
    {
        if (currentCommand != null)
        {
            ExecuteCommand();
        }

        victoryConditionManager.CheckPlayerVictory();
    }

    public void ExecuteCommand()
    {
        switch (currentCommand.commandType)
        {
            case CommandType.MoveTo:
                ExecuteMoveCommand();
                break;
            case CommandType.Attack:
                ExecuteAttackCommand();
                break;
            case CommandType.Wait:
                ExecuteWaitCommand();
                break;
        }
    }

    private void ExecuteMoveCommand()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Movement>().Move(currentCommand.path);
        receiver.GetComponent<CharacterTurn>().Walk = false;
        currentCommand = null;
        cleanUtility.ClearPathFinding();
        cleanUtility.ClearGridHighlightMove();
    }

    private void ExecuteAttackCommand()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<Attack>().AttackGridPosition(currentCommand.target);
        receiver.GetComponent<CharacterTurn>().Act = false;
        currentCommand = null;
        cleanUtility.ClearGridHighlightAttack();
    }

    private void ExecuteWaitCommand()
    {
        Character receiver = currentCommand.character;
        receiver.GetComponent<CharacterTurn>().Walk = false;
        receiver.GetComponent<CharacterTurn>().Act = false;
        currentCommand = null;
    }

    public void AddMoveCommand(Character character, Vector2Int selectedGrid, List<PathNode> path)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.MoveTo);
        currentCommand.path = path;
    }

    public void AddAttackCommand(Character attacker, Vector2Int selectedGrid, GridObject target)
    {
        currentCommand = new Command(attacker, selectedGrid, CommandType.Attack);
        currentCommand.target = target;
    }

    public void AddWaitCommand(Character character, Vector2Int selectedGrid)
    {
        currentCommand = new Command(character, selectedGrid, CommandType.Wait);
    }

    
}
