using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditionManager : MonoBehaviour
{
    [SerializeField] private ForceContainer enemyForce;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.EndCondition.CHECK_CONDITON, this.CheckPlayerVictory);
    }
    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.EndCondition.CHECK_CONDITON);
    }
    public void CheckPlayerVictory()
    {
        EnemyAnnahilationVictory();
    }

    private void EnemyAnnahilationVictory()
    {
        if (enemyForce.CheckDefeated())
        {
            EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_WINN);
            Debug.Log("WIN");
        }
    }
}
