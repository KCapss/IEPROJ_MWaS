using UnityEngine;

public class WeaponCardRewards : MonoBehaviour
{
    [SerializeField] private WeaponCard reward;
    [SerializeField] private WeaponCardObject rewardObject;

    public void SetReward()
    {
        BucketTier currentTier = GetTier();
        reward = PlayerData.Instance.GetRandomTieredWeapon(currentTier);
        rewardObject.ReceiveCardData(reward);
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

        return BucketTier.NONE;
    }
}
