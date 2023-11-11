using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    int owner = 0;

    public void SetOwner(Lane lane)
    { 
        owner = (int)lane;
    }
    private void OnAnimationEnd()
    {
        Parameters param = new Parameters();
        param.PutExtra(EventNames.AttackSequence.ATTACK_ANIMATION_END, owner);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ATTACK_ANIMATION_END, param);

        gameObject.SetActive(false);
    }
}
