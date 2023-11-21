using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyLibrary : MonoBehaviour
{
    public static EnemyLibrary Instance;

    [SerializeField] List<GameObject> tutorialEnemies;
    [SerializeField] List<GameObject> Level1Enemies;
    [SerializeField] List<GameObject> level2Enemies;
    [SerializeField] List<GameObject> level3Enemies;
    [SerializeField] List<GameObject> level4Enemies;

    [SerializeField] private int stageNumber = 0;
    [SerializeField] private LevelTracker tracker;
    
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

    public GameObject GetNextEnemyData()
    {
        var currentLevel = tracker.GetCurrentLevel();
        GameObject obj;

        switch (currentLevel)
        {
            case Levels.LEVEL_1:
                obj = Level1Enemies[stageNumber];
                break;

            case Levels.LEVEL_2:
                obj = level2Enemies[stageNumber];
                break;

            case Levels.LEVEL_3:
                obj = level3Enemies[stageNumber];
                break;

            case Levels.LEVEL_4:
                obj = level4Enemies[stageNumber];
                break;

            default:
                obj = tutorialEnemies[stageNumber];
                break;
        }

        stageNumber++;
        return obj;
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
        return 8 - stageNumber;
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
