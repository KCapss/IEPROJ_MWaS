using UnityEngine;

public class WeaponCard
{
    [Header("Weapon Stats")]
    [SerializeField] private string _name;
    [Tooltip("Displayed Name")]
    [SerializeField] private string _fullName;
    [SerializeField] private int _level;
    [SerializeField] private float _damageAdditional;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private int _restrictionValue;
    [SerializeField] private RestrictionType _restrictionType;
    [SerializeField] private float _cooldown;

    [Header("Weapon Components")]
    [SerializeField] private WeaponTag _weaponCardType;
    [SerializeField] private VFXTag _vfxWeaponTag;
    [SerializeField] private Sprite _cardIcon;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private Sprite _cardBackground;
    [SerializeField] private string _cardText;

    public string Name => _name;
    public int Level => _level;
    public float DamageAdditional => _damageAdditional;
    public float DamageMultiplier => _damageMultiplier;
    public int RestrictionValue => _restrictionValue;
    public RestrictionType RestrictionType => _restrictionType;
    public float Cooldown => _cooldown;
    public WeaponTag WeaponCardType => _weaponCardType;
    public VFXTag VFXWeaponTag => _vfxWeaponTag;
    public Sprite CardIcon => _cardIcon;
    public DamageType DamageType => _damageType;
    public Sprite CardBackground => _cardBackground;

    public string CardText => _cardText;

    public void Initialize(WeaponCard_SO weaponData, DamageType damageType, Sprite cardBackground)
    {
        _name = weaponData.Name;
        _level = weaponData.Level;
        _damageAdditional = weaponData.DamageAdditional;
        _damageMultiplier = weaponData.DamageMultiplier;
        _restrictionValue = weaponData.RestrictionValue;
        _restrictionType = weaponData.RestrictionType;
        _cooldown = weaponData.Cooldown;
        _weaponCardType = weaponData.WeaponCardType;
        _vfxWeaponTag= weaponData.VFXWeaponTag;
        _cardIcon = weaponData.CardIcon;
        _damageType = damageType;
        _cardBackground = cardBackground;
        _cardText = weaponData.CardText;
    }


    public int CalculateDamage(int damageCardValue, DamageType damageCardDamageType)
    {
        float damage = 0;

        damage = (DamageMultiplier * damageCardValue) + DamageAdditional;
        //.Log(_fullName + " calculated damage before floor = " + damage);

        int returnedDamage = 0;

        if(damageCardDamageType == _damageType)
        {                
            returnedDamage = Mathf.FloorToInt(damage * 1.5f);
            //Debug.Log(_fullName + " calculated damage after floor and crit = " + returnedDamage);
            return returnedDamage;
        }

        returnedDamage = Mathf.FloorToInt(damage);
        //Debug.Log(_fullName + " calculated damage after floor = " + returnedDamage);
        return returnedDamage;
    }
}
