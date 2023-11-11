using System.Collections.Generic;
using UnityEngine;

public class WeaponCardLibrary : MonoBehaviour
{
    Dictionary<WeaponTag,  WeaponCard_SO> weaponLibrary = new Dictionary<WeaponTag,  WeaponCard_SO>();

    [Header("Buckets")]
    [SerializeField] private List<WeaponCardBucket> weaponCardBucketList;
    [SerializeField] private List<WeaponCardTieredBucket> tieredWeaponCardBucketList;

    // Start is called before the first frame update
    void Awake()
    {   
        int i = 0;
        foreach(WeaponCardBucket bucket in weaponCardBucketList)
        {
            foreach(WeaponCard_SO card in bucket.list)
            {
                WeaponTag cardType = (WeaponTag)i;
                weaponLibrary[cardType] =  card;
                i++;
            }
        }
    }

    public WeaponCard_SO GetWeaponCardData(WeaponTag weaponType)
    {
        WeaponCard_SO cardData = weaponLibrary[weaponType];
        return cardData;
    }

    public  WeaponCard_SO GetRandomTieredWeaponData(BucketTier bucketTier)
    {
        int tierIndex = (int)bucketTier;
        int randomIndex = Random.Range(0, tieredWeaponCardBucketList[tierIndex].list.Count);

        WeaponTag type = tieredWeaponCardBucketList[tierIndex].list[randomIndex];
        WeaponCard_SO randomTieredWeapon = weaponLibrary[type];
        return randomTieredWeapon;
    }
}
