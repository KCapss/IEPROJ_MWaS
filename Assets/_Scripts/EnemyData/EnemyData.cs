using UnityEngine;

public class EnemyData
{
    private string _name;
    private int _hp;
    private int _damageBase;
    private int _damageIncrement;
    Sprite _sprite;
    private GameObject _animationSprite;

    public string Name { get { return _name; } set { _name = value;} }
    public int HP { get { return _hp; } set { _hp = value; } }
    public int DamageBase { get { return _damageBase; } set { _damageBase = value; } }
    public int DamageIncrement { get {return _damageIncrement; } set { _damageIncrement = value; } }
    public Sprite Sprite { get { return _sprite; } set { _sprite = value; }}
    public GameObject AnimationSprite { get { return _animationSprite; } set { _animationSprite = value; } }
}
