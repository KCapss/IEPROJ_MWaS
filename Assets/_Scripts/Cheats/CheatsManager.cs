using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
    public static CheatsManager Instance;
    private bool _debugDamage = false;
    public bool DebugDamage { get { return _debugDamage; } set { _debugDamage = value;} }


    private void Awake()
    {
        CreateSingleton();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    
}
