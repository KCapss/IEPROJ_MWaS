using UnityEngine;

public class UpgradeAllWeapons : MonoBehaviour
{
    public void UpgradeAllButton()
    {
        PlayerData.Instance.UpgradeAllWeapons();
        EventBroadcaster.Instance.PostEvent(EventNames.UI.DAMAGE_REWARD_OPEN);
    }
}
