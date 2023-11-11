using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CommandInput : MonoBehaviour
{
    CommandManager commandManager;
    MouseInput mouseInput;
    MoveCharacter moveCharacter;
    CharacterAttack characterAttack;
    SelectCharacter selectCharacter;
    CleanUtility cleanUtility;

    [SerializeField] private CommandType currentCommand;

    private bool isInputCommand;

    private void Awake()
    {
        commandManager = GetComponent<CommandManager>();
        mouseInput = GetComponent<MouseInput>();
        moveCharacter = GetComponent<MoveCharacter>();
        characterAttack = GetComponent<CharacterAttack>();
        selectCharacter = GetComponent<SelectCharacter>();
        cleanUtility = GetComponent<CleanUtility>();
    }

    private void Update()
    {
        if (isInputCommand == false) { return; }
        switch (currentCommand)
        {
            case CommandType.MoveTo:
                MoveCommandInput();
                break;
            case CommandType.Attack:
                AttackCommandInput();
                break;
            case CommandType.Wait:
                WaitCommandInput();
                break;
        }
    }

    public void InitCommand()
    {
        isInputCommand = true;
        switch (currentCommand)
        {
            case CommandType.MoveTo:
                HighlightWalkableTerrain();
                break;
            case CommandType.Attack:
                HighlightAttackArea();
                break;
            case CommandType.Wait:
                break;
        }
    }

    private void HighlightAttackArea()
    {
        characterAttack.CalculateAttackArea(
                    selectCharacter.selected.GetComponent<GridObject>().positionOnGrid,
                    selectCharacter.selected.AttackRange);
    }

    public void SetCommandType(CommandType commandType)
    {
        cleanUtility.ClearGridHighlightMove();
        cleanUtility.ClearPathFinding();
        currentCommand = commandType;
    }

    public void HighlightWalkableTerrain()
    {
        moveCharacter.CheckWalkableTerrain(selectCharacter.selected);
    }

    private void MoveCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<PathNode> path = moveCharacter.GetPath(mouseInput.positionOnGrid);
            if (path == null) { return;  }
            if (path.Count == 0) { return; }
            commandManager.AddMoveCommand(selectCharacter.selected, mouseInput.positionOnGrid, path);
            StopCurrentCommandInput();
        }
    }

    private void AttackCommandInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (characterAttack.Check(mouseInput.positionOnGrid))
            {
                GridObject gridObject = characterAttack.GetAttackTarget(mouseInput.positionOnGrid);
                if (gridObject == null) { return; }
                commandManager.AddAttackCommand(selectCharacter.selected, mouseInput.positionOnGrid, gridObject);
                StopCurrentCommandInput();
            }
        }
    }

    private void WaitCommandInput()
    {
        commandManager.AddWaitCommand(selectCharacter.selected, mouseInput.positionOnGrid);
        StopCurrentCommandInput();
    }

    public void StopCurrentCommandInput()
    {
        selectCharacter.Deselect();
        selectCharacter.enabled = true;
        isInputCommand = false;
        cleanUtility.ClearAll();
    }
}
