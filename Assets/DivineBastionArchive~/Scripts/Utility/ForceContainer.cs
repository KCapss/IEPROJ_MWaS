using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMember
{
    public Character character { get; set; }
    public CharacterTurn characterTurn { get; set; }

    public ForceMember(Character character, CharacterTurn characterTurn)
    {
        this.character = character;
        this.characterTurn = characterTurn;
    }
}

public class ForceContainer : MonoBehaviour
{
    public Factions faction;
    [SerializeField] public List<ForceMember> force;

    public void AddMe(CharacterTurn characterTurn)
    {
        if (force == null) { force = new List<ForceMember>(); }
        force.Add(new ForceMember(characterTurn.GetComponent<Character>(), characterTurn));
        characterTurn.transform.parent = transform;
    }

    public void GrantTurn()
    {
        for (int i = 0; i < force.Count; i++)
        {
            force[i].characterTurn.GrantTurn();
        }
    }

    public bool CheckDefeated()
    {
        for (int i = 0; i < force.Count; i++)
        {
            if (!force[i].character.isDefeated)
            {
                return false;
            }
        }

        return true;
    }
}
