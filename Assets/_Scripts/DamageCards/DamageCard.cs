using UnityEngine;

/* Scriptable Object for Mass Tests */
[CreateAssetMenuAttribute(menuName = "DCardStats", fileName = "New Damage Card")]
public class DamageCard : ScriptableObject
{
    [SerializeField] private DamageTag _damageTag;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private Sprite _cardBackground;
    [SerializeField] private int _damageValue;

    public Sprite CardBackground 
    {
        get { return _cardBackground; } 
        set { _cardBackground = value;} 
    }

    public DamageType DamageType
    { 
        get { return _damageType; }
        set { _damageType = value;}
    }

    public int DamageValue
    {
        get { return _damageValue; }
        set { _damageValue = value; }
    }

    public DamageTag DamageTag
    {
        get { return _damageTag; }
    }
}
