using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    struct ActionAndScore
    {
        public AttackMode action;
        public float score;
    }

    [Header("Behavior Multiplier")]
    [SerializeField] private float lightMultiplier;
    [SerializeField] private float heavyMultiplier;
    [SerializeField] private float skill_1Multiplier;
    [SerializeField] private float skill_2Multiplier;
    [SerializeField] private float skill_3Multiplier;
    [SerializeField] private float skill_4Multiplier;
    [SerializeField] private float defendMultiplier;

    [SerializeField] private float afterThresholdTimeMultiplier;
    [SerializeField] private float beforeThresholdTimeMultiplier;
    [SerializeField] private float stageMultiplier;

    [Header("Sensor")]
    [SerializeField] private Player playerReference;
    [SerializeField] private int playerHealth;
    [Tooltip("Time Elapsed Since Battle Start")]
    [SerializeField] private float timeElapsed;
    [SerializeField] private float timeBeforeAggro;


    private void Start()
    {
        playerReference = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        playerHealth = playerReference.HealthCurrent;
        timeElapsed += Time.deltaTime;
    }

    public AttackMode DecideNextAction()
    {
        List<ActionAndScore> actionsWithScores = new List<ActionAndScore>();

        foreach (AttackMode action in Enum.GetValues(typeof(AttackMode)))
        {
            float score = ScoreAction(action);
            actionsWithScores.Add(new ActionAndScore { action = action, score = score });
        }

        actionsWithScores.Sort((a, b) => b.score.CompareTo(a.score));

        int randomAction = UnityEngine.Random.Range(0, Mathf.Min(3, actionsWithScores.Count));

        return actionsWithScores[randomAction].action;
    }

    private float ScoreAction(AttackMode action)
    {
        float score = 0;
        float aggressiveMultiplier = beforeThresholdTimeMultiplier;
        if(timeElapsed >= timeBeforeAggro)
        {
            aggressiveMultiplier = afterThresholdTimeMultiplier;
        }

        switch (action)
        {
            case AttackMode.Light:
                score += playerHealth * lightMultiplier * aggressiveMultiplier;
                break;

            case AttackMode.Heavy:
                score += playerHealth * heavyMultiplier * aggressiveMultiplier;
                break;

            case AttackMode.Skill_1:
                score += playerHealth * skill_1Multiplier;
                break;

            case AttackMode.Skill_2:
                score += playerHealth * skill_2Multiplier;
                break;

            case AttackMode.Skill_3:
                score += playerHealth * skill_3Multiplier;
                break;

            case AttackMode.Skill_4:
                score += playerHealth * skill_4Multiplier;
                break;

            case AttackMode.Wait:
                score += playerHealth * defendMultiplier;
                break;

            default:
                break;
        }

        return score;
    }
}
