using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{

    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    [SerializeField] private int _damageBase;
    [SerializeField] private int _damageIncrement;
    [SerializeField] Sprite _image;

    public string Name { get { return _name; } }
    public int HP { get { return _hp; } }
    public int DamageBase { get { return _damageBase; } }
    public int DamageIncrement { get {return _damageIncrement; } }
    public Sprite Sprite { get { return _image; } }

}
