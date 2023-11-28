using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Boss : Enemy
{
    [Header("Enemy Components")]
    [SerializeField] private Enemy_AI decisionMaking;
    [SerializeField] private bool isShielded = false;
    [SerializeField] private DamageType currentWeakness;
    [SerializeField] private AttackMode previousAttackMode = AttackMode.None;
    private Coroutine currentCoroutine;
    private int damageInitial;

    private void OnEnable()
    {
        healthBar = GameObject.Find("Enemy_HP_UI").GetComponent<HealthBar>();
        SetStartingElement();
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

    private void SetStartingElement()
    {
        int randomValue = Random.Range(0, 2);

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (randomValue == 0)
        {
            spriteRenderer.color = Color.red;
            currentWeakness = DamageType.Fire;
        }
        else if (randomValue == 1)
        {
            spriteRenderer.color = Color.blue;
            currentWeakness = DamageType.Water;
        }
        else
        {
            spriteRenderer.color = Color.green;
            currentWeakness = DamageType.Wind;
        }
    }

    private void AI_Actuators(AttackMode action)
    {
        // Add SFX and VFX here probably
        previousAttackMode = action;
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

        float damage = DamageBase * Random.Range(0.85f, 1.0f);
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        currentCoroutine = StartCoroutine(TriggerCooldown(lightCooldown));
    }

    public override void HeavyAction()
    {
        if (animator != null)
        {
            Debug.LogWarning("Attack Heavy");
            animator.SetBool("isAttacking", true);
            //animator.SetBool("isAttacking", false);
        }

        float damage = DamageBase * 1.5f * Random.Range(0.85f, 1.0f);
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);
        currentCoroutine = StartCoroutine(TriggerCooldown(heavyCooldown));
    }

    public override void WaitAction()
    {
        currentCoroutine = StartCoroutine(TriggerCooldown(waitCooldown));
    }

    public override void Skill_1Action()
    {
        IncrementDamage();
        currentCoroutine = StartCoroutine(TriggerCooldown(skill_1Cooldown));
    }

    public override void Skill_2Action()
    {
        float damage = DamageBase * 0.25f;
        int nDamage = Mathf.Min(Mathf.FloorToInt(damage), damageInitial * 2);
        GameManager.Instance.battleManager.DealDamage(Faction.Player, Mathf.FloorToInt(nDamage), DamageType.NONE);
        EventBroadcaster.Instance.PostEvent(EventNames.AttackSequence.ENEMY_ATTACK);

        IncrementDamage();

        currentCoroutine = StartCoroutine(TriggerCooldown(skill_2Cooldown));
    }

    public override void Skill_3Action()
    {
        StartCoroutine(ActivateShield());
        currentCoroutine = StartCoroutine(TriggerCooldown(skill_3Cooldown));
    }

    public override void Skill_4Action()
    {
        ChangeWeakness();
        currentCoroutine = StartCoroutine(TriggerCooldown(skill_3Cooldown));
    }

    IEnumerator ActivateShield()
    {
        isShielded = true;

        Parameters param = new Parameters();
        param.PutExtra(EventNames.UI.SHIELDS_UP, skill_3Cooldown);
        EventBroadcaster.Instance.PostEvent(EventNames.UI.SHIELDS_UP, param);

        yield return new WaitForSeconds(skill_3Cooldown);
        isShielded = false;
    }

    public override void ReceiveDamage(int damage, DamageType damageType)
    {
        if(isShielded) { return; }

        if(damageType == currentWeakness)
        {
            ResetCooldown();
        }

        base.ReceiveDamage(damage, damageType);
        healthBar.UpdateHealthBar(HealthCurrent, HealthMax);
    }

    public void ResetCooldown()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        switch(previousAttackMode)
        {
            case AttackMode.Light:
                currentCoroutine = StartCoroutine(TriggerCooldown(lightCooldown));
                break;

            case AttackMode.Heavy:
                currentCoroutine = StartCoroutine(TriggerCooldown(heavyCooldown));
                break;

            case AttackMode.Skill_1:
                currentCoroutine = StartCoroutine(TriggerCooldown(skill_1Cooldown));
                break;

            case AttackMode.Skill_2:
                currentCoroutine = StartCoroutine(TriggerCooldown(skill_2Cooldown));
                break;

            case AttackMode.Skill_3:
                currentCoroutine = StartCoroutine(TriggerCooldown(skill_3Cooldown));
                break;

            case AttackMode.Skill_4:
                currentCoroutine = StartCoroutine(TriggerCooldown(skill_4Cooldown));
                break;

            case AttackMode.Wait:
                currentCoroutine = StartCoroutine(TriggerCooldown(waitCooldown));
                break;
        }
    }

    public void ChangeWeakness()
    {
        int randomValue = Random.Range(0, 2);

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if(randomValue == 0)
        {
            if(currentWeakness == DamageType.Fire)
            {
                currentWeakness = DamageType.Water;
                spriteRenderer.color = Color.blue;
            }
            else
            {
                currentWeakness = DamageType.Fire;
                spriteRenderer.color = Color.red;
            }
        }
        else if(randomValue == 1)
        {
            if(currentWeakness == DamageType.Water)
            {
                currentWeakness = DamageType.Wind;
                spriteRenderer.color = Color.green;
            }
            else
            {
                currentWeakness = DamageType.Water;
                spriteRenderer.color = Color.blue;
            }
        }

        else if(randomValue == 2)
        {
            if(currentWeakness == DamageType.Wind)
            {
                currentWeakness = DamageType.Fire;
                spriteRenderer.color = Color.red;
            }
            else
            {
                currentWeakness = DamageType.Wind;
                spriteRenderer.color = Color.green;
            }
        }

    }

}
