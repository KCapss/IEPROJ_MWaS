using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Factions
{
    Player = 0,
    Opponent

}

public class CharacterTurn : MonoBehaviour
{
    private bool canWalk;
    private bool canAct;
    private Factions faction;

    public bool Walk
    { 
        get { return canWalk;} 
        set { canWalk = value;}
    }
    public bool Act 
    { 
        get { return canAct; } 
        set { canAct = value;}
    }
    public Factions Faction 
    {
        get { return faction; }
        set { faction = value;}
    }

    private void Start()
    {
        AddToRoundManager();
        GrantTurn();
    }
    public void GrantTurn()
    {
        canWalk = true;
        canAct = true;
    }

    public void AddToRoundManager()
    {
        RoundManager.instance.AddMe(this);
    }
}
