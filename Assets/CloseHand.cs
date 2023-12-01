using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseHand : MonoBehaviour
{
    [SerializeField] private GameObject hand;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.UI.WEAPON_REWARD_OPEN, CloseHandObject);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.DAMAGE_REWARD_OPEN);
    }

    public void CloseHandObject()
    {
        hand.SetActive(false);
    }
}
