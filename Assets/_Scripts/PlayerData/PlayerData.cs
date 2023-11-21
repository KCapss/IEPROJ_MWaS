using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Player Max HP per Level")]
    [SerializeField] private List<int> maxHP;

    [Header("Weapon Data")] // 0 = Left, 1 = mid, 2 = right
    [SerializeField] private List<WeaponTag> weaponList;

    [Header("Weapon Elements")] // 0 = Left, 1 = mid, 2 = right
    [SerializeField] private List<DamageType> damageTypes;

    [Header("Deck Data")]
    [SerializeField] private List<DamageTag> deckElements;

    [Header("Elements")]
    [SerializeField] private List<Sprite> elementSprites;

    public static PlayerData Instance;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        for(int i = 0; i < damageTypes.Count; i++)
        {
            damageTypes[i] = (DamageType)UnityEngine.Random.Range(0, 3);
        }
    }

    void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Gets Data of Damage Cards in Deck
    public List<DamageCard> GetDeckElements()
    {
        List<DamageCard> data = new List<DamageCard>();

        foreach(DamageTag element in deckElements)
        {
            data.Add(CardLibrary.Instance.DamageLibrary.GetData(element));
        }

        return data;
    }

    // Returns List of Active Weapons (Active = Currently Equipped By Player)
    public List<WeaponCard> GetWeapons()
    {
        List<WeaponCard> data = new List<WeaponCard>();

        for(int i = 0; i < weaponList.Count; i++)
        {
            WeaponCard weaponCard = new WeaponCard();

            WeaponCard_SO weaponCardData = CardLibrary.Instance.WeaponLibrary.GetWeaponCardData(weaponList[i]);
            DamageType damageType = damageTypes[i];
            Sprite cardBackground = GetElementCardSprite(damageType);

            weaponCard.Initialize(weaponCardData, damageType, cardBackground);
            data.Add(weaponCard);
        }

        return data;
    }

    // Returns a Weapon from a Specified Tier (Used By Rewards System)
    public WeaponCard GetRandomTieredWeapon(BucketTier bucketTier)
    {
        WeaponCard card = new WeaponCard();
        WeaponCard_SO cardData = CardLibrary.Instance.WeaponLibrary.GetRandomTieredWeaponData(bucketTier);
        DamageType newDamageType = (DamageType)UnityEngine.Random.Range(0, 3);
        Sprite cardBackground = GetElementCardSprite(newDamageType);

        card.Initialize(cardData, newDamageType, cardBackground);
        return card;
    }

    public void UpgradeAllWeapons()
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            UpgradeWeapon((Lane)i);
        }
    }

    public void ReplaceCurrentWeapon(Lane lane, WeaponTag newWeapon, DamageType newDamageType)
    {
        damageTypes[(int)lane] = newDamageType;
        weaponList[(int)lane] = newWeapon;

        for(int i = 0; i < damageTypes.Count; i++)
        {
            if(i != (int)lane) //NotReplaced
            {
                UpgradeWeapon((Lane)i);
            }
        }
    }

    public void UpgradeWeapon(Lane lane)
    {
        int index = (int)lane;
        int currentWeaponIndex = (int)weaponList[index];

        if(currentWeaponIndex % 8 != 7) // Not in the end of the upgrade +7 is the max upgrade
        {
            currentWeaponIndex++;
            weaponList[index] = (WeaponTag)currentWeaponIndex;
        }
    }

    public Sprite GetElementCardSprite(DamageType type)
    {
        int index = (int)type;
        return elementSprites[index];
    }

    public int GetMaxHP()
    {
        int currentLevel = EnemyLibrary.Instance.GetCurrentStageNumber();
        return maxHP[currentLevel];
    }

    public void AddToDeck(List<DamageCard> cardList)
    {
        foreach(DamageCard card in cardList)
        {
            deckElements.Add(card.DamageTag);
        }
    }
}
