using System.Collections.Generic;
using UnityEngine;

public class DamageCardLibrary : MonoBehaviour
{
    [Header("Available Cards")]
    [SerializeField] private List<DamageCard> damageCards;
    [SerializeField] private Dictionary<DamageTag,DamageCard> damageLibrary = new Dictionary<DamageTag, DamageCard>();

    [Header("Buckets")]
    [SerializeField] private List<DamageCardBucketList> damageCardBucketList;

    private void Awake()
    {
        for(int i = 0; i < damageCards.Count; i++)
        {
            damageLibrary.Add((DamageTag)i, damageCards[i]);
        }
    }

    public DamageCard GetData(DamageTag cardType)
    {
        return damageLibrary[cardType];
    }

    public DamageCardBucket GetRandomBucket(BucketTier bucketTier) 
    {
        int tierIndex = (int)bucketTier;
        DamageCardBucketList selectedBucketList = damageCardBucketList[tierIndex];
        int randomBucketIndex = Random.Range(0, selectedBucketList.list.Count);
        DamageCardBucket resultBucket = selectedBucketList.list[randomBucketIndex];

        return resultBucket;
    }

    public DamageCardBucket GetDamageCardBucket(BucketTier tier)
    {
        DamageCardBucket cardBucket = GetRandomBucket(tier);
        return cardBucket;
    }
}
