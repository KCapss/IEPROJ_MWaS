using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyAnimationControllerLibrary : MonoBehaviour
{

    [Header("Enemy Animator Libary")]
    [SerializeField] 
    private AnimatorControllerDict aControllerDict;

    public Dictionary<string, AnimatorController> enemyAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        LoadEnemyAnimatorController();
    }

    private void LoadEnemyAnimatorController()
    {
        enemyAnimator = new Dictionary<string, AnimatorController>();
        enemyAnimator = aControllerDict.ToDictionary();
    }

}

[Serializable]
public class AnimatorControllerDict
{
    [SerializeField] 
    private AnimatorControllerDictItems[] aControllerItems;

    public Dictionary<string, AnimatorController> ToDictionary()
    {
        Dictionary<string, AnimatorController> newAnimatorControllersDict = new Dictionary<string, AnimatorController>();

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
    public AnimatorController controller;
}
