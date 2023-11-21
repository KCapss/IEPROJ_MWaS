using System;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine.Animations;
using UnityEngine;

public class EnemyAnimationControllerLibrary : MonoBehaviour
{

    [Header("Enemy Animator Libary")]
    [SerializeField] 
    private AnimatorControllerDict aControllerDict;

    public Dictionary<string, RuntimeAnimatorController> enemyAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        LoadEnemyAnimatorController();
    }

    private void LoadEnemyAnimatorController()
    {
        enemyAnimator = new Dictionary<string, RuntimeAnimatorController>();
        enemyAnimator = aControllerDict.ToDictionary(); }

}

[Serializable]
public class AnimatorControllerDict
{
    [SerializeField] 
    private AnimatorControllerDictItems[] aControllerItems;

    public Dictionary<string, RuntimeAnimatorController> ToDictionary()
    {
        Dictionary<string, RuntimeAnimatorController> newAnimatorControllersDict = new Dictionary<string, RuntimeAnimatorController>();

        foreach (var item in aControllerItems)
        {
            newAnimatorControllersDict.Add(item.name, item.controller);
        }

        return newAnimatorControllersDict;
    }
}


[Serializable]
public class AnimatorControllerDictItems
{
    [SerializeField]
    public string name;

    [SerializeField]
    public RuntimeAnimatorController controller;
}
