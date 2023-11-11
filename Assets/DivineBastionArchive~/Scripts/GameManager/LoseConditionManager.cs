using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseConditionManager : MonoBehaviour
{
    [SerializeField] private ForceContainer playerForce;
    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.CHECK_CONDITON, this.CheckEnemyVictory);
    }
    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.CHECK_CONDITON);
    }
    public void CheckEnemyVictory()
    {
        if (playerForce.CheckDefeated())
        {
            EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_LOSE);
            Debug.Log("LOSE");
        }
    }
}
