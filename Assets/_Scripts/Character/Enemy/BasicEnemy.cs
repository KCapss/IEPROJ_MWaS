using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BasicEnemy : Enemy
{
    [Header("Enemy Components")]
    [SerializeField] private Enemy_AI decisionMaking;

    private void OnEnable()
    {
        healthBar = GameObject.Find("Enemy_HP_UI").GetComponent<HealthBar>();
       
    }

    private void Update()
    {
        if (!isOnCooldown && !isDead)
        {
            AttackMode nextAction = decisionMaking.DecideNextAction();
            AI_Actuators(nextAction);
        }
    }

    private void AI_Actuators(AttackMode action)
    {
        // Add SFX and VFX here probably
        DoAttackAction(action);
    }

    private void DoAttackAction(AttackMode attackAction)
    {
        if (attackActions.ContainsKey(attackAction))
        {
            attackActions[attackAction]();
        }
    }

    public override void Kill() 
    {
        //this will trigger the death animation and progress to next enemies
        Debug.LogError("Enemy Died");
        StartCoroutine(DeathSequence());
    } 

    IEnumerator DeathSequence()
    {
        isDead = true;
        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_COMBAT_END);
        yield return new WaitForSecondsRealtime(3.0f);
        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_WINN);
    }

    public void IncrementDamage()
    {
        Debug.Log("Enemy Stat Up");
        _damageBase += _damageIncrement;
    }

    public override void LightAction()
    {
        //Add Animation Here
        if (animator != null)
        {
            Debug.LogWarning("Attack Light");
            animator.SetBool("isAttacking", true);
            //animator.SetBool("isAttacking", false);
        }


        GameManager.Instance.battleManager.DealDamage(Faction.Player, DamageBase);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        StartCoroutine(TriggerCooldown(lightCooldown));
    }

    public override void HeavyAction()
    {
        if (animator != null)
        {
            Debug.LogWarning("Attack Heavy");
            animator.SetBool("isAttacking", true);
            //animator.SetBool("isAttacking", false);
        }

        float damage = DamageBase * 1.5f;
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(damage));
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        StartCoroutine(TriggerCooldown(heavyCooldown));
    }

    public override void WaitAction()
    {
        Debug.Log("Wait");
        StartCoroutine(TriggerCooldown(waitCooldown));
    }

    public override void Skill_1Action()
    {
        IncrementDamage();
        StartCoroutine(TriggerCooldown(skill_1Cooldown));
    }

    public override void Skill_2Action()
    {
        float damage = DamageBase * 0.25f;
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(damage));
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);

        IncrementDamage();

        StartCoroutine(TriggerCooldown(skill_2Cooldown));
    }

    public override void Skill_3Action()
    {
    }

    public override void Skill_4Action()
    {
    }

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        healthBar.UpdateHealthBar(HealthCurrent, HealthMax);
    }


}
