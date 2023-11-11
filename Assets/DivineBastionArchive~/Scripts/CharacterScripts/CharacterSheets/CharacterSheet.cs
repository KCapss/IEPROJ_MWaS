using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSheet : ScriptableObject
{
    [SerializeField] public GameObject sprite;
    [SerializeField] public string Name;
    [SerializeField] public float movementPoints; // 10 points = 1 tile
    [SerializeField] public Int2Val HP;
    [SerializeField] public int damage;
    [SerializeField] public int attackRange;
    [SerializeField] public Factions faction;
    [SerializeField] public int DEX;
    [SerializeField] public int DEF;
    [SerializeField] public MyEnums.ItemDropType DropType;
    [SerializeField] public int Money;
}
