using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.AttackSequence.ENEMY_ATTACK, PlayAnimation);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.AttackSequence.ENEMY_ATTACK);
    }

    public void PlayAnimation()
    {
        animator.SetTrigger(MyStrings.TriggerAnimation);
    }

    public void ResetAnimation()
    {
        animator.ResetTrigger(MyStrings.TriggerAnimation);
    }

}
