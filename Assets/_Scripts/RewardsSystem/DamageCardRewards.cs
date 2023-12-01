using System.Collections.Generic;
using UnityEngine;

public class DamageCardRewards : MonoBehaviour
{
    [SerializeField] private List<GameObject> slots;

    [Header("Read Only")]
    [SerializeField] private DamageCardBucket rewards;
    [SerializeField] private List<DamageCard> cards;

    public void SetReward()
    {
        BucketTier currentTier = GetTier();
        rewards = CardLibrary.Instance.DamageLibrary.GetDamageCardBucket(currentTier);
        
        //Debug.Log(rewards._name);

        for(int i = 0; i < slots.Count; i++)
        {
            DamageCard damageCard = CardLibrary.Instance.DamageLibrary.GetData(rewards.cards[i]);
            cards.Add(damageCard);

            GameObject obj = GameManager.Instance.objectPoolManager.GetPooledObject(PoolTag.DamageCard);
            obj.SetActive(true);

            DamageCardObject damageCardObject = obj.GetComponent<DamageCardObject>();
            damageCardObject.SetCardData(damageCard);

            obj.transform.SetParent(slots[i].transform);
            obj.transform.position = slots[i].transform.position;
        }
    }

    private BucketTier GetTier()
    {
        int currentTier = EnemyLibrary.Instance.GetCurrentStageNumber();

        switch(currentTier)
        {
            case 1: case 2: return BucketTier.Tier1;

            case 3: case 4: return BucketTier.Tier2;

            case 5: case 6: return BucketTier.Tier3;

            case 7: return BucketTier.Tier4;
        }

        return BucketTier.Tier5;
    }

    //Tied To A Button
    public void RewardBucketPressed()
    {
        PlayerData.Instance.AddToDeck(cards);
        GameManager.Instance.levelManager.LoadNextLevel();
    }
}
