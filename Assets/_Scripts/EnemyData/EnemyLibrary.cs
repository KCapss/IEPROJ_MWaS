using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyLibrary : MonoBehaviour
{
    public static EnemyLibrary Instance;

    [SerializeField] List<EnemyStats> enemyLibrary;
    [SerializeField] private int stageNumber = 0;
    
    //Libary for the Animation:
    private Dictionary<string, AnimatorController> enemAnimatorController;

    private void Awake()
    {
        CreateSingleton();
    }


    public void Initialize()
    {
        enemAnimatorController = new Dictionary<string, AnimatorController>();
        enemAnimatorController = this.gameObject.GetComponent<EnemyAnimationControllerLibrary>().enemyAnimator;
    }

    public EnemyData GetNextEnemyData()
    {
        EnemyData data = new EnemyData();

        data.Name = enemyLibrary[stageNumber].Name;
        data.HP = enemyLibrary[stageNumber].HP;
        data.DamageBase = enemyLibrary[stageNumber].DamageBase;
        data.DamageIncrement = enemyLibrary[stageNumber].DamageIncrement;
        data.Sprite = enemyLibrary[stageNumber].Sprite;
        data.AnimationSprite = enemyLibrary[stageNumber].AnimationSprite;
        stageNumber++;

        return data;
    }

    public AnimatorController RetrieveEnemyAnimatorController(string key)
    {
        if(enemAnimatorController == null)
            Initialize();

        AnimatorController enemAnimator = enemAnimatorController[key];

        if (enemAnimator)
            return enemAnimator;

        else
        {
            Debug.LogError($"No Animator Controller for {key}");
            return null;
        }
    }

    public int GetCurrentStageNumber() 
    {
        return stageNumber;
    }

    public void ResetCurrentStageNumber()
    {
        Debug.Log("RESET");
        stageNumber = 0;
    }

    public int GetRemainingStageCount()
    {
        return enemyLibrary.Count;
    }

    private void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        Debug.Log("Enemy Library Initialized");

        DontDestroyOnLoad(gameObject);
    }
}
