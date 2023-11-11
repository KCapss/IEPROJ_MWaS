using UnityEngine;

[CreateAssetMenuAttribute(menuName = "WCardStats", fileName = "New Weapon Card")]
[System.Serializable]
public class WeaponCard_SO : ScriptableObject
{
    [Header("Weapon Stats")]
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private float _damageAdditional;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private int _restrictionValue;
    [SerializeField] private RestrictionType _restrictionType;
    [SerializeField] private float _cooldown;

    [Header("Weapon Components")]
    [SerializeField] private WeaponTag _weaponCardType;
    [SerializeField] private Sprite _cardIcon;
    [SerializeField] private string _cardText;

    public string Name => _name;
    public int Level => _level;
    public float DamageAdditional => _damageAdditional;
    public float DamageMultiplier => _damageMultiplier;
    public int RestrictionValue => _restrictionValue;
    public RestrictionType RestrictionType => _restrictionType;
    public float Cooldown => _cooldown;
    public WeaponTag WeaponCardType => _weaponCardType; 
    public Sprite CardIcon => _cardIcon;
    public string CardText => _cardText;
}
