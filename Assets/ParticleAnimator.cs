using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimator : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSys;
    int owner = 0;
    // Start is called before the first frame update
    void Start()
    {
        particleSys = this.gameObject.GetComponent<ParticleSystem>();
    }
    public void SetOwner(Lane lane)
    {
        owner = (int)lane;
    }

    private void Update()
    {
        if (!particleSys.isPlaying)
        {
            Parameters param = new Parameters();
            param.PutExtra(EventNames.AttackSequence.ATTACK_ANIMATION_END, owner);
            EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ATTACK_ANIMATION_END, param);

            gameObject.SetActive(false);
        }
    }

}
