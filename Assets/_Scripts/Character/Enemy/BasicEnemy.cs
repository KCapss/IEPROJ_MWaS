using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BasicEnemy : Enemy
{
    [Header("Enemy Components")]
    [SerializeField] private Enemy_AI decisionMaking;
    private int damageInitial;

    private void OnEnable()
    {
        healthBar = GameObject.Find("Enemy_HP_UI").GetComponent<HealthBar>();
        damageInitial = DamageBase;
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
        if(!isDead)
        {
            StartCoroutine(DeathSequence());
        }
    } 

    IEnumerator DeathSequence()
    {
        isDead = true;
        yield return StartCoroutine(TriggerDeathAnim());
        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_COMBAT_END);
        yield return new WaitForSecondsRealtime(3.0f);
        EventBroadcaster.Instance.PostEvent(EventNames.EndCondition.ON_WINN);
    }

    IEnumerator TriggerDeathAnim()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Material material = spriteRenderer.material;
        float timer = 0f;
        while (timer < 1.0f)
        {
            float fillAmount = 1.0f - timer;
            material.SetFloat("_FullDistortionFade", fillAmount);

            timer += Time.deltaTime;
            Debug.Log(fillAmount);
            yield return null;
        }

    }

    public void IncrementDamage()
    {
        if (animator != null && isBuffing)
        {
            animator.SetBool("isBuffing", true);
        }
        Debug.Log("Enemy Stat Up");
        _damageBase += _damageIncrement;
    }

    public override void LightAction()
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", true);
        }

        float damage = DamageBase * Random.Range(0.85f, 1.0f);
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        StartCoroutine(TriggerCooldown(lightCooldown));
    }

    public override void HeavyAction()
    {
        if (animator != null)
        {
            if (isAttackingHeavy)
                animator.SetBool("isAttackingHeavy", true);
            else
            {
                animator.SetBool("isAttacking", true);
            }

        }

        float damage = DamageBase * 1.5f * Random.Range(0.85f, 1.0f);
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        StartCoroutine(TriggerCooldown(heavyCooldown));
    }

    public override void WaitAction()
    {
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
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
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

    public override void ReceiveDamage(int damage, DamageType damageType)
    {
        base.ReceiveDamage(damage, damageType);
        healthBar.UpdateHealthBar(HealthCurrent, HealthMax);
    }


}
