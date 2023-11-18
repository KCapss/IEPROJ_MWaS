using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Enemy : Character
{
    [Header("Damage")]
    [SerializeField] protected int _damageBase;
    [SerializeField] protected int _damageIncrement;

    [Header("Components")]
    /* Speed? as variable for all monster */
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    [Header("HEALTH BAR UI")]
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected bool isDead = false;

    [Header("Attack Cooldowns")]
    [SerializeField] protected float lightCooldown = 1.0f;
    [SerializeField] protected float heavyCooldown = 2.0f;
    [SerializeField] protected float skill_1Cooldown = 1.5f;
    [SerializeField] protected float skill_2Cooldown = 1.5f;
    [SerializeField] protected float skill_3Cooldown = 1.5f;
    [SerializeField] protected float skill_4Cooldown = 1.5f;
    [SerializeField] protected float waitCooldown = 1.5f;
    [SerializeField] protected bool isOnCooldown = false;

    [Header("Animation Sprite")] //Exclusive for Enemies
    [SerializeField] protected GameObject spriteAnimationObject;

    protected delegate void AttackAction();
    protected Dictionary<AttackMode, AttackAction> attackActions = new Dictionary<AttackMode, AttackAction>();

    public int DamageBase
    {
        get => _damageBase;
        set => _damageBase = value;
    }

    public int DamageIncrement
    {
        get => _damageIncrement;
        set => _damageIncrement = value;
    }

    private void Start()
    {
        InitializeAttackActions();
        StartCoroutine(TriggerCooldown(2.0f));
    }

    private void OnEnable()
    {
        EnemyData data = EnemyLibrary.Instance.GetNextEnemyData();
        TransferStats(data);

        HealthCurrent = HealthMax;
    }

    private void TransferStats(EnemyData enemyData)
    {
        _healthMax = enemyData.HP;
        _name = enemyData.Name;
        _damageBase = enemyData.DamageBase;
        _damageIncrement = enemyData.DamageIncrement;
        _sprite = enemyData.Sprite;

        //Checker if the ff:
        // 1. Sprite Chop is present
        // 2. AnimatorController IsPresent
        //
        if (enemyData.AnimationSprite != null && 
            EnemyLibrary.Instance.RetrieveEnemyAnimatorController(_name) != null)
        {
            spriteRenderer.sprite = null;
            Debug.Log("Animation Sprite Found");

            spriteAnimationObject = enemyData.AnimationSprite;
            Instantiate(spriteAnimationObject, this.gameObject.transform, false);

            spriteAnimationObject.AddComponent<Animator>();
            spriteAnimationObject.GetComponent<Animator>().runtimeAnimatorController = 
                EnemyLibrary.Instance.RetrieveEnemyAnimatorController(_name);
        }

        else
        {
            Debug.Log("No Animation Sprite Found");
            spriteRenderer.sprite = _sprite;
        }

        

        InitiateCharacter();
    }

    private void InitiateCharacter()
    {
        if (animator = gameObject.GetComponent<Animator>())
        {
            Debug.Log($"Found Animator {_name}");
        }
    }

    private void InitializeAttackActions()
    {
        attackActions[AttackMode.Light] = LightAction;
        attackActions[AttackMode.Heavy] = HeavyAction;
        attackActions[AttackMode.Skill_1] = Skill_1Action;
        attackActions[AttackMode.Skill_2] = Skill_2Action;
        attackActions[AttackMode.Skill_3] = Skill_3Action;
        attackActions[AttackMode.Skill_4] = Skill_4Action;
        attackActions[AttackMode.Wait] = WaitAction;
    }

    public abstract void LightAction();

    public abstract void HeavyAction();

    public abstract void WaitAction();

    public abstract void Skill_1Action();

    public abstract void Skill_2Action();

    public abstract void Skill_3Action();

    public abstract void Skill_4Action();

    public override void ReceiveDamage(int damage)
    {
        base.ReceiveDamage(damage);
        healthBar.UpdateHealthBar(HealthCurrent, HealthMax);
    }

    protected IEnumerator TriggerCooldown(float cooldownTimer)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        isOnCooldown = false;
    }
}

[Flags]
public enum AttackMode
{
    None = 0,
    Light,
    Heavy,
    Skill_1,
    Skill_2,
    Skill_3,
    Skill_4,
    Wait
}