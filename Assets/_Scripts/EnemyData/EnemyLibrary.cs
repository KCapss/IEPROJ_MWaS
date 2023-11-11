using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    public static EnemyLibrary Instance;

    [SerializeField] List<EnemyStats> enemyLibrary;
    [SerializeField] private int stageNumber = 0;

    private void Awake()
    {
        CreateSingleton();
    }

    public EnemyData GetNextEnemyData()
    {
        EnemyData data = new EnemyData();

        data.Name = enemyLibrary[stageNumber].Name;
        data.HP = enemyLibrary[stageNumber].HP;
        data.DamageBase = enemyLibrary[stageNumber].DamageBase;
        data.DamageIncrement = enemyLibrary[stageNumber].DamageIncrement;
        data.Sprite = enemyLibrary[stageNumber].Sprite;
        stageNumber++;

        return data;
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
